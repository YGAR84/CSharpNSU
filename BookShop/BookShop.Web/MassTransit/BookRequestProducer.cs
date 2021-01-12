using System.Threading.Tasks;
using BookShop.Web.Contracts;
using BookShop.Web.Extensions;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookShop.Web.MassTransit
{
	public class BookRequestProducer
	{
		private readonly ISendEndpointProvider _sendEndpointProvider;
		private readonly IConfiguration _configuration;

		public BookRequestProducer(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
		{
			_sendEndpointProvider = sendEndpointProvider;
			_configuration = configuration;
		}

		public async Task SentBookRequestEvent(int numOfBooks)
		{
			var message = new BookRequestContract()
			{
				NumOfBooks = numOfBooks
			};

			var hostConfig = _configuration.GetMassTransitConfiguration();
			var endpoint = await _sendEndpointProvider
				.GetSendEndpoint(hostConfig.GetQueueAddress(Constants.RequestQueue));

			await endpoint.Send(message);
		}
    }
}
