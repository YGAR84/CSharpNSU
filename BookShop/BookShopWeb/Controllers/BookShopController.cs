using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookShop;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookShopController : ControllerBase
	{
		private readonly IBookServiceProxy _bookShopServiceProxy;

		public BookShopController(IBookServiceProxy serviceProxy)
		{
			_bookShopServiceProxy = serviceProxy;
		}

		[HttpGet]
		[Route("{count}")]
		public async Task<List<Book>> Get(int count)
		{
			return await _bookShopServiceProxy.GetBooks(count);
		}

	}
}
