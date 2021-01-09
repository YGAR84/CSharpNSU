using System.Collections.Generic;
using BookShop.ContractLibrary;
using BookShop.Logic.Requests;
using BookShop.Logic.Requests.BookRequests;

namespace BookShop.Logic.Extensions
{
	internal static class BookExtension
	{
		public static AddBookRequest ToAddBookRequest(this IBook book)
		{
			return new AddBookRequest()
			{
				ArriveDate = book.DateOfDelivery,
				Author = "no author",
				Cost = book.Price,
				Genres = new List<string> { book.Genre },
				Title = book.Title
			};
		}
	}
}