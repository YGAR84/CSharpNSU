using System.Collections.Generic;
using BookShop.ContractLibrary;

namespace BookShop.BooksProvider.Contracts
{
	public class BookResponseContract : IBookResponseContract
	{
		public List<IBook> Books { get; set; }
	}
}
