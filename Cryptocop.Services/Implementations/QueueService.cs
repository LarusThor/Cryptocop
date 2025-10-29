using Cryptocop.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using IModel = Microsoft.EntityFrameworkCore.Metadata.IModel;

namespace Cryptocop.Services.Implementations;

public class QueueService : IQueueService, IDisposable
{
    private readonly IConnection _connection;
    private readonly RabbitMQ.Client.IModel _channel;

    public QueueService()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }
    public Task PublishMessageAsync(string routingKey, object body)
    {
        var json =  JsonConvert.SerializeObject(body);
        var bodyBytes = Encoding.UTF8.GetBytes(json);
        
        _channel.BasicPublish("", routingKey, null, bodyBytes);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}