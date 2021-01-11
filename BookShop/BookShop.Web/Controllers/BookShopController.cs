using System.Threading.Tasks;
using BookShop.Logic;
using BookShop.Logic.Requests.BookShopRequest;
using BookShop.Logic.Responses.BookShopResponses;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers
{
	[Route("api/bookShop")]
	[ApiController]
	public class BookShopController : ControllerBase
	{
		private readonly BookShopService _bookShopService;

		public BookShopController(BookShopService bookShopService)
		{
			_bookShopService = bookShopService;
		}

		[HttpGet]
		public async Task<GetBookShopResponse> GetBookShopState()
		{
			return await _bookShopService.GetBookShopResponse();
		}

		[HttpPost]
		[Route("increase/storage")]
		public async Task IncreaseStorage([FromBody] ChangeStorageBookShopRequest changeStorageRequest)
		{
			await _bookShopService.IncreaseStorage(changeStorageRequest);
		}

		[HttpPost]
		[Route("increase/money")]
		public async Task IncreaseMoney([FromBody] ChangeMoneyBookShopRequest changeMoneyRequest)
		{
			await _bookShopService.IncreaseMoney(changeMoneyRequest);
		}

		[HttpPost]
		[Route("decrease/storage")]
		public async Task DecreaseStorage([FromBody] ChangeStorageBookShopRequest changeStorageRequest)
		{
			await _bookShopService.DecreaseStorage(changeStorageRequest);
		}

		[HttpPost]
		[Route("decrease/money")]
		public async Task DecreaseMoney([FromBody] ChangeMoneyBookShopRequest changeMoneyRequest)
		{
			await _bookShopService.DecreaseMoney(changeMoneyRequest);
		}
	}
}
