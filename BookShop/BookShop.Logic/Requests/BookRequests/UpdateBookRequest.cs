using System;
using System.Collections.Generic;

namespace BookShop.Logic.Requests.BookRequests
{
	public class UpdateBookRequest
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public decimal Cost { get; set; } = -1;
		public DateTime ArriveDate { get; set; }
		public List<string> Genres { get; set; }
	}
}
