using System.Threading.Tasks;
using BookShop.ContractLibrary;
using BookShop.Logic;
using MassTransit;

namespace BookShop.Web.MassTransit
{
	public class BooksReceivedConsumer : IConsumer<IBookResponseContract>
	{
		private readonly BookShopService _bookShopService;
		public BooksReceivedConsumer(BookShopService bookShopService)
		{
			_bookShopService = bookShopService;
		}
        public async Task Consume(ConsumeContext<IBookResponseContract> context)
        {
	        var message = context.Message;
            await _bookShopService.AddBooks(message.Books);
        }
    }
}
