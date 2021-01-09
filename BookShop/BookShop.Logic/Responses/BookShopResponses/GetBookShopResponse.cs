namespace BookShop.Logic.Responses.BookShopResponses
{
	public class GetBookShopResponse
	{
		public decimal Balance { get; set; }
		public int StorageSize { get; set; }
		public int CurrentBookCount { get; set; }
	}
}