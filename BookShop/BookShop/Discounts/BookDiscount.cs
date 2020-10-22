using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Discounts
{
	public class BookDiscount : Discount
	{
		public BookInfo BookInfo { get; }

		public BookDiscount(DateTime expireDate, int discountPercentage, BookInfo bookInfo) : base(expireDate,
			discountPercentage)
		{
			BookInfo = bookInfo;
		}

		protected override bool HasDiscount(Book book)
		{
			return book.BookInfo == BookInfo;
		}
	}
}
