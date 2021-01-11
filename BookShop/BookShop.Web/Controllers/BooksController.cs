using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Logic;
using BookShop.Logic.Requests;
using BookShop.Logic.Requests.BookRequests;
using BookShop.Logic.Responses.BookResponses;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers
{
	[Route("api/books")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookServiceProxy _bookShopServiceProxy;
		private readonly BookShopService _bookShopService;

		public BooksController(IBookServiceProxy serviceProxy, BookShopService bookShopService)
		{
			_bookShopServiceProxy = serviceProxy;
			_bookShopService = bookShopService;
		}

		[HttpGet]
		public async Task<List<GetBooksResponse>> GetBooks()
		{
			return await _bookShopService.GetBooksResponse();
		}
		
		[HttpGet( "{bookGuid}")]
		public async Task<GetBookResponse> GetBook(Guid bookGuid)
		{
			return await _bookShopService.GetBookResponse(bookGuid);
		}

		[HttpPost]
		public async Task AddBook([FromBody] AddBookRequest addBookRequest)
		{
			await _bookShopService.AddBook(addBookRequest);
		}

		[HttpPost("add/{count}")]
		public async Task AddBooks(int count)
		{
			var addBookRequests = await _bookShopServiceProxy.GetAddBookRequests(count);
			await _bookShopService.AddBooks(addBookRequests);
		}


		[HttpPut("{bookGuid}")]
		public async Task UpdateBook(Guid bookGuid, [FromBody] UpdateBookRequest updateBookRequest)
		{
			await _bookShopService.UpdateBook(bookGuid, updateBookRequest);
		}

		[HttpDelete("{bookGuid}")]
		public async Task DeleteBook(Guid bookGuid)
		{
			await _bookShopService.DeleteBook(bookGuid);
		}

		[HttpPost("buy/{bookGuid}")]
		public async Task BuyBook(Guid bookGuid)
		{
			await _bookShopService.SellBook(bookGuid);
		}
	}
}
