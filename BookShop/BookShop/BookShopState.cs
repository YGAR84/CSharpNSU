using System;
using System.Collections.Generic;
using BookShop.Core.Discounts;

namespace BookShop.Core
{
	public class BookShopState
	{
		public int Id { get; set; }
		public decimal Balance { get; set; }
		public int StorageSize { get; set; }

	}
}