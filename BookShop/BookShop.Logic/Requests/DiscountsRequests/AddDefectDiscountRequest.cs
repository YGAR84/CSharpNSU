using System;

namespace BookShop.Logic.Requests.DiscountsRequests
{
	public class AddDefectDiscountRequest : AddDiscountRequest
	{
		public Guid BookGuid { get; set; }
	}
}
