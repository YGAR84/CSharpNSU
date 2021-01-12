using System;

namespace BookShop.Logic.Responses.DiscountsResponses
{
	public class GetDefectDiscountResponse : GetDiscountResponse
	{
		public Guid BookGuid { get; set; }
	}
}
