using System;
using System.Collections.Generic;
using BookShop.Core;
using BookShop.Core.Discounts;
using FluentAssertions;
using NUnit.Framework;

namespace BookShop.Test.DiscountsTest
{
	public class GenreDiscountTest
	{
		[Test]
		public void GenreDiscountCtorTest()
		{
			List<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			decimal cost = 500.1m;
			DateTime arriveDate = DateTime.Today.Date;

			Book book = new Book(bookInfo1, cost, arriveDate);

			var expireDate = DateTime.Today.AddDays(1000).Date;
			Discount discount = new GenreDiscount(expireDate, 80, new Genre("fantasy"));
			discount.ExpireDate.Should().Be(expireDate);
		}

		[Test]
		public void BookDiscountApplyDiscountTest()
		{
			List<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			decimal cost = 500.11m;

			Book book1 = new Book(bookInfo1, cost, DateTime.Today.Date);

			List<Genre> genres2 = new List<Genre> { new Genre("other") };
			BookInfo bookInfo2 = new BookInfo("One Flew Over the Cuckoo's Nest", " Ken Kesey", genres2);
			Book book2 = new Book(bookInfo2, 600m, DateTime.Today.AddDays(-110));

			var expireDate = DateTime.Today.AddDays(1000).Date;
			Discount discount = new GenreDiscount(expireDate, 80, new Genre("fantasy"));

			discount.ApplyDiscount(book1, DateTime.Today.Date).Cost.Should().Be(100.02m);
			discount.ApplyDiscount(book1, DateTime.Today.AddDays(1001).Date).Cost.Should().Be(book1.Cost);
			discount.ApplyDiscount(book2, DateTime.Today.Date).Cost.Should().Be(book2.Cost);
		}
	}
}