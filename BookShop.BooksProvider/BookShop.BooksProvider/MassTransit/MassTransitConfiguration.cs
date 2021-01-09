using System;

namespace BookShop.BooksProvider.MassTransit
{
    public class MassTransitConfiguration
    {
        public string RabbitMqAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Durable { get; set; }
        public bool PurgeOnStartup { get; set; }
        public string ResponseQueue { get; set; }
        public string RequestQueue { get; set; }

        public Uri GetQueueAddress(string queueName)
        {
            return new Uri(RabbitMqAddress + "/" + queueName);
        }
    }
}
