using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Logic.Responses.DiscountsResponses
{
	public class GetGenreDiscountResponse : GetDiscountResponse
	{
		public string GenreName { get; set; }
	}
}
