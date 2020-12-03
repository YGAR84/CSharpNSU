using System;
using System.Collections.Generic;
using BookShop.Core;
using FluentAssertions;
using NUnit.Framework;

namespace BookShop.Test
{
	[TestFixture]
	public class BookTest
	{
		[Test]
		public void BookCtorTest()
		{
			List<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			decimal cost = 500.1m;
			DateTime arriveDate = DateTime.Today.Date; 
			Book book1 = new Book(bookInfo1, cost, arriveDate);

			book1.Cost.Should().Be(cost);
			book1.ArriveDate.Should().Be(arriveDate);
			book1.BookInfo.Should().Be(bookInfo1);
		}
	}
}