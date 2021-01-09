using System;
using System.Threading.Tasks;
using BookShop.Logic;
using BookShop.Web.Contracts;
using BookShop.Web.Extensions;
using BookShop.Web.MassTransit;
using JetBrains.Annotations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace BookShop.Web.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class BooksCountCheckJob  : IJob
    {
	    private readonly BookShopService _bookShopService;
		private readonly ISendEndpointProvider _sendEndpointProvider;
		private readonly IConfiguration _configuration;
		public BooksCountCheckJob(BookShopService bookShopService, ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
	    {
		    _bookShopService = bookShopService;
		    _sendEndpointProvider = sendEndpointProvider;
		    _configuration = configuration;
		}

	    public async Task Execute(IJobExecutionContext context)
	    {
		    var needBooks = await _bookShopService.NeedMoreBooks();
		    if (needBooks)
            {
	            var count = await _bookShopService.GetBooksGetCount();
	            if (count <= 0)
	            {
		            return;
	            }

	            var message = new BookRequestContract()
	            {
					NumOfBooks = count
	            };

	            var hostConfig = _configuration.GetMassTransitConfiguration();
	            var endpoint = await _sendEndpointProvider.GetSendEndpoint(hostConfig.GetQueueAddress(hostConfig.RequestQueue));
	            await endpoint.Send(message);

            }
	    }
    }
}