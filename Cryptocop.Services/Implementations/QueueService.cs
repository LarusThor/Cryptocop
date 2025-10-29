using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class QueueService : IQueueService, IDisposable
{
    public Task PublishMessageAsync(string routingKey, object body)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}