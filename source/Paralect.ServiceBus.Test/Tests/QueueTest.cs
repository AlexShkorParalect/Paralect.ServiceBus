using System;
using NUnit.Framework;
using Paralect.ServiceBus.InMemory;
using Paralect.ServiceBus.Msmq;
using Paralect.ServiceBus.Test.Messages;

namespace Paralect.ServiceBus.Test.Tests
{
    [TestFixture]
    public class QueueTest
    {
        [Test]
        public void PurgingWhenJustCreated()
        {
            Helper.CreateAndOpenQueue(queue =>
            {
                queue.Purge();
            });
        }        
        
        [Test]
        public void SendAndReceiveSingleMessage()
        {
            Helper.CreateAndOpenQueue(queue =>
            {
                var transportMessage = new TransportMessage(new object[]
                {
                    new Message1("MessageName", 2011)
                });

                queue.Send(queue.Provider.TranslateToQueueMessage(transportMessage));

                Helper.AssertTransportMessage(transportMessage,
                    queue.Provider.TranslateToTransportMessage(queue.Receive(TimeSpan.FromSeconds(5))));
            },
            new IQueueProvider[]
            {
                new MsmqQueueProvider(),
                new InMemoryQueueProvider()
            });
        }      
  
        [Test]
        public void SendAndReceiveMultipleMessage()
        {
            Helper.CreateAndOpenQueue(queue =>
            {
                var transportMessage = new TransportMessage(new object[]
                {
                    new Message1("MessageName1", 2011),
                    new Message2("MessageName2", 2012),
                    new Message1("MessageName3", 2013),
                    new Message2("MessageName4", 2014),
                    new Message2("MessageName5", 2015),
                });

                queue.Send(queue.Provider.TranslateToQueueMessage(transportMessage));

                Helper.AssertTransportMessage(transportMessage,
                    queue.Provider.TranslateToTransportMessage(queue.Receive(TimeSpan.FromSeconds(5))));
            },
            new IQueueProvider[]
            {
                new MsmqQueueProvider(),
                new InMemoryQueueProvider()
            });
        }  

        [Test]
        public void SendTwiceAndReceiveMultipleMessage()
        {
            Helper.CreateAndOpenQueue(queue =>
            {
                var transportMessage = new TransportMessage(new object[]
                {
                    new Message1("MessageName1", 2011),
                    new Message2("MessageName2", 2012),
                    new Message1("MessageName3", 2013),
                    new Message2("MessageName4", 2014),
                    new Message2("MessageName5", 2015),
                });

                queue.Send(queue.Provider.TranslateToQueueMessage(transportMessage));
                queue.Send(queue.Provider.TranslateToQueueMessage(transportMessage));

                Helper.AssertTransportMessage(transportMessage,
                    queue.Provider.TranslateToTransportMessage(queue.Receive(TimeSpan.FromSeconds(5))));

                Helper.AssertTransportMessage(transportMessage,
                    queue.Provider.TranslateToTransportMessage(queue.Receive(TimeSpan.FromSeconds(5))));
            },
            new IQueueProvider[]
            {
                new MsmqQueueProvider(),
                new InMemoryQueueProvider()
            });
        }
    }
}