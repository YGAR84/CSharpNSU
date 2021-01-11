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
        
        #warning обычно названия очередей хранятся "где-нибудь" (класс c константами, нугет-пакет с названиями очередей и тп)
        #warning я бы их отсюда убрал, ты же когда подключаешь к RMQ, можешь работать куда с большим кол-вом очередей, чем только с этими
        public string ResponseQueue { get; set; }
        public string RequestQueue { get; set; }

        public Uri GetQueueAddress(string queueName)
        {
            return new Uri(RabbitMqAddress + "/" + queueName);
        }
    }
}
