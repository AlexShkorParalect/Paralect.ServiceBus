using System;

namespace Paralect.ServiceBus
{
    public interface IBus : IDisposable
    {
        void Send(params Object[] messages);
        void SendLocal(params Object[] messages);
        void Publish(Object message);

        void Run();
        void Wait();
    }
}

/*

    TransportMessage message = _queue.Manager.ConvertToTransportMessage(queueMessage);

    if (message.Messages[0].GetType() == typeof(ShutdownMessage))
    {
        var shutdown = (ShutdownMessage) message.Messages[0];
        if (shutdown.Token != _shutdownToken)
            continue;

        break;
    }
 
 
    _log.Info("Received message {0} from sender {1}@{2}",
        message.GetType().FullName,
        message.SentFromComputerName,
        message.SentFromQueueName);
 * 
 * 
         public void Stop()
        {
            var queueMessage = _queue.Manager.ConvertToQueueMessage(
                new TransportMessage(new ShutdownMessage() { Token = _shutdownToken }));
            _queue.Send(queueMessage);
        }

 
*/