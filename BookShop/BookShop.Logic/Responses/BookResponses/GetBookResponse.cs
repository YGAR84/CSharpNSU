using System;
using System.Collections.Generic;

namespace BookShop.Logic.Responses.BookResponses
{
	public class GetBookResponse
	{
		public Guid Guid { get; set; }
		public int BookInfoId { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public decimal Cost { get; set; }
		public DateTime ArriveDate { get; set; }
		public List<string> Genres { get; set; }
	}
}
