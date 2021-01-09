using System;
using System.Threading.Tasks;
using BookShop.BooksProvider.ExternalServices;
using BookShop.ContractLibrary;
using MassTransit;

namespace BookShop.BooksProvider.MassTransit
{
	public class BooksReceivedRequestConsumer : IConsumer<IBookRequestContract>
	{
		private readonly BookResponseProducer _bookResponseProducer;
		private readonly ServiceProxy _serviceProxy;

		public BooksReceivedRequestConsumer(ServiceProxy serviceProxy, BookResponseProducer booksResponseProducer)
        {
	        _bookResponseProducer = booksResponseProducer;
	        _serviceProxy = serviceProxy;
        }
        public async Task Consume(ConsumeContext<IBookRequestContract> context)
        {
	        var message = context.Message;
	        if (message.NumOfBooks <= 0) return;

	        var books = await _serviceProxy.GetBooks(message.NumOfBooks);
	        await _bookResponseProducer.SentBookRequestEvent(books);

        }
    }
}