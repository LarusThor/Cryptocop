using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendGrid;
using SendGrid.Helpers.Mail;
using Cryptocop.Models.Dtos;

namespace Cryptocop.Worker.Emails
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private IConnection? _connection;
        private IModel? _channel;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
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

            _channel.QueueDeclare(queue: "email-queue", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: "email-queue", exchange: "amq.direct", routingKey: "create-order");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                try
                {
                    var order = JsonSerializer.Deserialize<OrderDto>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (order != null)
                    {
                        _logger.LogInformation("Received order for {Email}", order.Email);
                        await SendEmail(order);
                    }
                    else
                    {
                        _logger.LogWarning("⚠Could not deserialize order message.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing email message.");
                }
            };
            
            _channel.BasicConsume(queue: "email-queue", autoAck: true, consumer: consumer);

            _logger.LogInformation("Email Worker listening on 'email-queue'...");

            await Task.CompletedTask;
        }

        private async Task SendEmail(OrderDto order)
        {
            var apiKey = _config["SendGrid:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                _logger.LogError("Missing SendGrid API Key in configuration.");
                return;
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("no-reply@cryptocop.com", "Cryptocop");
            var to = new EmailAddress(order.Email, order.FullName);
            var subject = "Your Cryptocop Order Confirmation";

            var htmlContent = $@"
                <h2>Thank you for your order, {order.FullName}!</h2>
                <p><strong>Date:</strong> {order.OrderDate}</p>
                <p><strong>Shipping to:</strong><br>
                {order.StreetName} {order.HouseNumber}<br>
                {order.City}, {order.ZipCode}<br>
                {order.Country}</p>
                <p><strong>Total Price:</strong> ${order.TotalPrice:F2}</p>
                <h3>Order Items:</h3>
                <ul>
                    {string.Join("", order.OrderItems.Select(i => $"<li>{i.ProductIdentifier} — {i.Quantity} × ${i.UnitPrice:F2}</li>"))}
                </ul>
            ";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            var response = await client.SendEmailAsync(msg);

            _logger.LogInformation("Email sent to {Email}: {Status}", order.Email, response.StatusCode);
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
