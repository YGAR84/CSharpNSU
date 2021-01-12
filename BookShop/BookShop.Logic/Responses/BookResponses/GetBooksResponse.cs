using System.Collections.Generic;

namespace BookShop.Logic.Responses.BookResponses
{
	public class GetBooksResponse
	{
		public int BookInfoId { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public List<string> Genres { get; set; }

		public List<InnerBook> Books { get; set; }
	}
}
