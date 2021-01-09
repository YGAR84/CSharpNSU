using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Logic;
using BookShop.Logic.Requests.DiscountsRequests;
using BookShop.Logic.Responses.DiscountsResponses;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers
{
	[Route("api/discounts")]
	[ApiController]
	public class DiscountsController : ControllerBase
	{
		private readonly BookShopService _bookShopService;

		public DiscountsController(BookShopService bookShopService)
		{
			_bookShopService = bookShopService;
		}

		[HttpGet]
		public async Task<List<GetDiscountResponse>> GetDiscounts()
		{
			return await _bookShopService.GetDiscountsResponses();
		}

		[HttpGet( "{discountId}")]
		public async Task<GetDiscountResponse> GetDiscount(int discountId)
		{
			return await _bookShopService.GetDiscountResponse(discountId);
		}

		[HttpGet("book/{bookGuid}")]
		public async Task<List<GetDiscountResponse>> GetDiscountForBook(Guid bookGuid)
		{
			return await _bookShopService.GetDiscountResponsesForBook(bookGuid);
		}

		[HttpPost]
		public async Task AddDiscount([FromBody] AddDiscountRequest addDiscountRequest)
		{
			await _bookShopService.AddDiscount(addDiscountRequest);
		}

		[HttpPost]
		[Route("defect")]
		public async Task AddDefectDiscount([FromBody] AddDefectDiscountRequest addDiscountRequest)
		{
			await _bookShopService.AddDiscount(addDiscountRequest);
		}

		[HttpPost]
		[Route("book")]
		public async Task AddBookDiscount([FromBody] AddBookDiscountRequest addDiscountRequest)
		{
			await _bookShopService.AddDiscount(addDiscountRequest);
		}

		[HttpPost]
		[Route("genre")]
		public async Task AddGenreDiscount([FromBody] AddGenreDiscountRequest addDiscountRequest)
		{
			await _bookShopService.AddDiscount(addDiscountRequest);
		}

		[HttpDelete("{discountId}")]
		public async Task DeleteDiscount(int discountId)
		{
			await _bookShopService.DeleteDiscount(discountId);
		}
    }
}
