using System;

namespace Paralect.ServiceBus
{
    public interface IEndpoint : IDisposable
    {
        /// <summary>
        /// Queue name
        /// </summary>
        EndpointAddress Name { get; }

        /// <summary>
        /// Queue manager which create this queue
        /// </summary>
        IEndpointProvider Provider { get; }

        /// <summary>
        /// Delete all messages from this queue
        /// </summary>
        void Purge();

        /// <summary>
        /// Send message to this queue
        /// </summary>
        void Send(EndpointMessage message);

        /// <summary>
        /// Blocking call. 
        /// </summary>
        EndpointMessage Receive(TimeSpan timeout);
    }
}