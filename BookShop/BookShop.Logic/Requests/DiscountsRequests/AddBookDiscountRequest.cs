namespace BookShop.Logic.Requests.DiscountsRequests
{
	public class AddBookDiscountRequest : AddDiscountRequest
	{
		public int BookInfoId { get; set; }
	}
}
