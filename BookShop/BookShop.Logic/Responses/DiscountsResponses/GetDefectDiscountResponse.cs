using System;
using System.Collections.Generic;
using System.Text;
using BookShop.Core;

namespace BookShop.Logic.Responses.DiscountsResponses
{
	public class GetDefectDiscountResponse : GetDiscountResponse
	{
		public Guid BookGuid { get; set; }
	}
}
