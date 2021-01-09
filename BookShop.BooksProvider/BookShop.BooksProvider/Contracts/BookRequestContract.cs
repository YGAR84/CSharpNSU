using BookShop.ContractLibrary;

namespace BookShop.BooksProvider.Contracts
{
	public class BookRequestContract : IBookRequestContract
	{
		public int NumOfBooks { get; set; }
	}
}
