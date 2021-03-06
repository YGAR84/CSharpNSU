﻿using System;
using System.Linq;

namespace BookShop.Core.Discounts
{
	public class GenreDiscount : Discount
	{
		public int GenreId { get; set; }
		public Genre Genre { get; set; }

		public GenreDiscount(){}
		public GenreDiscount(DateTime expireDate, int discountPercentage, Genre genre) : base(expireDate, discountPercentage)
		{
			Genre = genre;
		}

		protected override bool HasDiscount(Book book)
		{
			return book.BookInfo.Genres
				.Select(g => g.Id)
				.Contains(GenreId);
		}
	}
}
