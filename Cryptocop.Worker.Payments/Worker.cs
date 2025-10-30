using System.Text;
using System.Text.Json;
using CreditCardValidator;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cryptocop.Worker.Payments
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnection? _connection;
        private IModel? _channel;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(10000, stoppingToken);
            
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "payment-queue", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: "payment-queue", exchange: "amq.direct", routingKey: "create-order");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<OrderMessage>(json);

                var detector = new CreditCardDetector(order.CreditCard);
                bool isValid = detector.IsValid();
                _logger.LogInformation("Card {card} validation result: {result}", order.CreditCard, isValid ? "VALID" : "INVALID");

            };

            _channel.BasicConsume(queue: "payment-queue", autoAck: true, consumer: consumer);
            await Task.CompletedTask;
        }
    }

    public class OrderMessage
    {
        public string CreditCard { get; set; } = null!;
        public string CardholderName { get; set; } = null!;
        public float TotalPrice { get; set; }
    }
}
