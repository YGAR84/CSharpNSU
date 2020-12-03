using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Core;
using BookShop.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookShopController : ControllerBase
	{
		private readonly IBookServiceProxy _bookShopServiceProxy;

		private readonly BookShopContextDbContextFactory _dbContextFactory;
		public BookShopController(IBookServiceProxy serviceProxy, BookShopContextDbContextFactory dbContextFactory)
		{
			_bookShopServiceProxy = serviceProxy;
			_dbContextFactory = dbContextFactory;
		}

		[HttpGet("/a")]
		public string Get()
		{
			return "sdaad";
		}

		[HttpGet]
		[Route("{count}")]
		public async Task<List<Book>> Get(int count)
		{
			return await _bookShopServiceProxy.GetBooks(count);
		}

		[HttpGet]
		[Route("/books")]
		public async Task<List<Book>> GetBooks()
		{
			await using var context = _dbContextFactory.GetContext();
			return await context.GetBooks();
		}

	}
}
