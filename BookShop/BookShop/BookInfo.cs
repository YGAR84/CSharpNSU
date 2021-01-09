using System.Collections.Generic;
using BookShop.Core.Discounts;

namespace BookShop.Core
{
	public class BookInfo
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }

		public virtual List<Genre> Genres { get; set; }
		public virtual List<Book> Books { get; set; }

		public virtual List<BookDiscount> BookDiscounts { get; set; }

		public BookInfo(){}

		public BookInfo(string title, string author, List<Genre> genres)
		{
			Title = title;
			Author = author;
			Genres = genres;
		}
	}
}
