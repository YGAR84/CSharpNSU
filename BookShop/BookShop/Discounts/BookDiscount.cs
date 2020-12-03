using System;

namespace BookShop.Core.Discounts
{
	public class BookDiscount : Discount
	{
		public int BookInfoId { get; set; }
		public BookInfo BookInfo { get; set; }

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
