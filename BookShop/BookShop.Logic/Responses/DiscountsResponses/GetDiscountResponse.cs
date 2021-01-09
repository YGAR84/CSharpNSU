using System;

namespace BookShop.Logic.Responses.DiscountsResponses
{
	public class GetDiscountResponse
	{
		public int Id { get; set; }
		public DateTime ExpireDate { get; set; }
		public decimal DiscountPercentage { get; set; }

	}
}
