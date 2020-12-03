using System;

namespace BookShop.Core.Discounts
{
	public class GenreDiscount : Discount
	{
		public int GenreId { get; set; }
		public Genre Genre { get; }
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
