using System;

namespace BookShop.Logic.Responses.BookResponses
{
	public class InnerBook
	{
		public Guid Guid { get; set; }
		public decimal Cost { get; set; }
		public DateTime ArriveDate { get; set; }
	}
}
