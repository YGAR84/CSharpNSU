using System;
using System.Linq;

namespace BookShop.Discounts
{
	public class GenreDiscount : Discount
	{
		private Genre Genre { get; }

		public GenreDiscount(DateTime expireDate, int discountPercentage, Genre genre) : base(expireDate, discountPercentage)
		{
			Genre = genre;
		}

		protected override bool HasDiscount(Book book)
		{
			return book.BookInfo.Genres.Contains(Genre);
		}
	}
}
