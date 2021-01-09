using System;

namespace BookShop.Logic.Requests.DiscountsRequests
{
	public class AddDiscountRequest
	{
		public DateTime ExpireDate { get; set; }
		public decimal DiscountPercentage { get; set; }

	}
}
