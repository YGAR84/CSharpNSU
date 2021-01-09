using System;
using System.Collections.Generic;

namespace BookShop.Logic.Requests.BookRequests
{
	public class AddBookRequest
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public decimal Cost { get; set; }
		public DateTime ArriveDate { get; set; }
		public List<string> Genres { get; set; }

		//public Book ToBook()
		//{
		//	Book book = new Book();
		//	book.ArriveDate = ArriveDate;
		//	book.Cost = Cost;
			
		//	BookInfo bookInfo = new BookInfo();
		//	bookInfo.Author = Author;
		//	bookInfo.Title = Title;

		//	List<Genre> genres = new List<Genre>();


		//	return book;
		//}
	}
}
