using System;
using System.Collections.Generic;
using BookShop.Core.Discounts;

namespace BookShop.Core
{
	public class Book
	{
		public Guid Guid { get; set; }
		public decimal Cost { get; set; }
		public DateTime ArriveDate { get; set; }

		public int BookInfoId { get; set; }
		public BookInfo BookInfo { get; set; }

		public List<DefectDiscount> DefectDiscounts { get; set; }
		public Book(BookInfo bookInfo, decimal cost, DateTime arriveDate)
		{
			Guid = Guid.NewGuid();
			Cost = Math.Round(cost, 2);
			ArriveDate = arriveDate;
			BookInfo = bookInfo;
		}
	}
}