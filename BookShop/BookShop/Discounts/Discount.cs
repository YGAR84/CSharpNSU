using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Discounts
{
	public abstract class Discount
	{
		public DateTime ExpireDate { get; }
		public decimal DiscountPercentage { get; }

		protected Discount(DateTime expireDate, int discountPercentage)
		{
			ExpireDate = expireDate;
			DiscountPercentage = (discountPercentage % 101) / 100m;
		}

		protected Book BookFromBookAndCost(Book book, decimal newCost)
		{
			return new Book(book.BookInfo, newCost, book.ArriveDate);
		}

		public bool IsExpired(DateTime currentDate)
		{
			return ExpireDate < currentDate;
		}

		protected abstract bool HasDiscount(Book book);

		public Book ApplyDiscount(Book book, DateTime currentDate)
		{
			if (IsExpired(currentDate) || !HasDiscount(book)) return book;

			decimal bookCost = book.Cost;
			decimal newBookCost = Math.Round(bookCost * (1 - DiscountPercentage), 2);

			return BookFromBookAndCost(book, newBookCost);
		}
	}
}
