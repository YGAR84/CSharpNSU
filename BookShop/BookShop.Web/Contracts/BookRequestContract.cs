using BookShop.ContractLibrary;

namespace BookShop.Web.Contracts
{
	public class BookRequestContract : IBookRequestContract
	{
		public int NumOfBooks { get; set; }
	}
}
