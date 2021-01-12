using System.Threading.Tasks;
using BookShop.Logic;
using BookShop.Web.MassTransit;
using JetBrains.Annotations;
using Quartz;

namespace BookShop.Web.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class BooksCountCheckJob  : IJob
    {
	    private readonly BookShopService _bookShopService;
	    private readonly BookRequestProducer _bookRequestProducer;
		public BooksCountCheckJob(BookShopService bookShopService, BookRequestProducer bookRequestProducer)
	    {
		    _bookShopService = bookShopService;
		    _bookRequestProducer = bookRequestProducer;
	    }

	    public async Task Execute(IJobExecutionContext context)
	    {
		    var needBooks = await _bookShopService.NeedMoreBooks();
		    if (needBooks)
            {
	            var count = await _bookShopService.GetBooksOrderCount();
	            if (count <= 0)
	            {
		            return;
	            }

	            await _bookRequestProducer.SentBookRequestEvent(count);
            }
	    }
    }
}