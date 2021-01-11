using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Infrastructure
{
	#warning это ещё что за?))
	public class Programm
	{
		public static void Main(string[] args)
		{
			var bshp = new BookShop.Infrastructure.EntityFramework.BookShopContextDbContextFactory(null).GetContext();
			var sa = bshp.GetBooks();
		}
	}
}
