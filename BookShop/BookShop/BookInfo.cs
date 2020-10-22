using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop
{
	public class BookInfo
	{
		public string Title { get; }
		public string Author { get; }
		public IReadOnlyList<Genre> Genres { get; }

		public BookInfo(string title, string author, IReadOnlyList<Genre> genres)
		{
			Title = title;
			Author = author;
			Genres = genres;
		}
	}
}
