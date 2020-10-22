using System;
using System.Collections.Generic;

namespace BookShop
{
	public class Book
	{
		public Guid Guid { get; }
		public BookInfo BookInfo { get; }
		public decimal Cost { get; }

		public DateTime ArriveDate { get; }


		public Book(BookInfo bookInfo, decimal cost, DateTime arriveDate)
		{
			Guid = Guid.NewGuid();
			Cost = Math.Round(cost, 2);
			ArriveDate = arriveDate;
			BookInfo = bookInfo;
		}
	}
}