using System.Text;
using System.Text.Json;
using CreditCardValidator;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cryptocop.Worker.Payments;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IConnection? _connection;
    private RabbitMQ.Client.IModel? _channel;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        InitializeRabbitMq();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory(){ HostName = "rabbitmq", UserName = "guest", Password = "guest" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        
        channel.QueueDeclare("payment-queue", true, false, false);
        channel.QueueBind("payment-queue", "amq.direct", "create-order");
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var order = JsonSerializer.Deserialize<OrderMessage>(json);

            var detector = new CreditCardDetector(order.CreditCard);
        };
        channel.BasicConsume("payment-queue", true, consumer);
        return Task.CompletedTask;
    }
    
    public class OrderMessage
    {
        public string CreditCard { get; set; } = null!;
        public string CardholderName { get; set; } = null!;
        public float TotalPrice { get; set; }
    }
}
