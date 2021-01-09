using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookShop.Logic.Responses.DiscountsResponses
{
	public class GetBookDiscountResponse : GetDiscountResponse
	{
		public int BookInfoId { get; set; }
	}
}
