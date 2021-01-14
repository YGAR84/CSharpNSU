using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.BooksProvider.Contracts;
using BookShop.BooksProvider.Extensions;
using BookShop.ContractLibrary;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookShop.BooksProvider.MassTransit
{
	public class BookResponseProducer
	{
		private readonly ISendEndpointProvider _sendEndpointProvider;
		private readonly IConfiguration _configuration;

		public BookResponseProducer(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
		{
			_sendEndpointProvider = sendEndpointProvider;
			_configuration = configuration;
		}

		public async Task SentBookRequestEvent(List<IBook> books)
		{
			var message = new BookResponseContract()
			{
				Books = books
			};

			var hostConfig = _configuration.GetMassTransitConfiguration();
			var endpoint = await _sendEndpointProvider
				.GetSendEndpoint(hostConfig.GetQueueAddress(Constants.RequestQueue));

			await endpoint.Send(message);
		}
    }
}
