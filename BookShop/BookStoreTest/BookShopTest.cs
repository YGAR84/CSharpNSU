using System;
using System.Collections.Generic;
using BookShop.Core;
using BookShop.Core.Discounts;
using FluentAssertions;
using NUnit.Framework;

namespace BookShop.Test
{
	[TestFixture]
	public class BookShopTest
	{
		[Test]
		public void BookShopCtor1Test()
		{

			BookStorage bookStorage = new BookStorage();
			BookShop.Core.BookShop bookStore = new BookShop.Core.BookShop(bookStorage, DateTime.Now.Date);

			bookStore.Balance.Should().Be(0);
			bookStore.BookStorage.Should().Be(bookStorage);
			bookStore.CurrentDate.Date.Should().Be(DateTime.Now.Date);
		}

		[Test]
		public void BookShopCtor2Test()
		{
			decimal balance = 50;
			BookShop.Core.BookShop bookStore = new BookShop.Core.BookShop(DateTime.Now.Date, balance);

			bookStore.Balance.Should().Be(balance);
			bookStore.BookStorage.Should().NotBeNull();
			bookStore.CurrentDate.Date.Should().Be(DateTime.Now.Date);
		}

		[Test]
		public void BookShopAddBookTest()
		{
			List<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			decimal cost = 600m;
			DateTime arriveDate = DateTime.Today.Date;
			Book book1 = new Book(bookInfo1, cost, arriveDate);

			BookShop.Core.BookShop bookShop = new BookShop.Core.BookShop(DateTime.Now.Date);

			bookShop.AddBook(book1);
			bookShop.SellBook(book1.Guid);

			bookShop.Balance.Should().Be(book1.Cost);

			bookShop.SellBook(book1.Guid);

			bookShop.Balance.Should().Be(book1.Cost);
		}

		[Test]
		public void BookShopAddDiscountTest()
		{
			List<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			decimal cost = 600m;
			DateTime arriveDate = DateTime.Today.Date;
			Book book1 = new Book(bookInfo1, cost, arriveDate);

			List<Genre> genres2 = new List<Genre> { new Genre("other") };
			BookInfo bookInfo2 = new BookInfo("One Flew Over the Cuckoo's Nest", " Ken Kesey", genres2);
			Book book2 = new Book(bookInfo2, 600m, DateTime.Today.AddDays(-110));

			BookShop.Core.BookShop bookShop = new BookShop.Core.BookShop(DateTime.Today.Date);

			bookShop.AddBook(book1);
			bookShop.AddBook(book2);

			Discount defectDiscount = new DefectDiscount(DateTime.Today.Date.AddDays(400).Date, 100, book1.Guid);
			bookShop.AddDiscount(defectDiscount);

			bookShop.Discounts.Should().Contain(defectDiscount);
			bookShop.Discounts.Count.Should().Be(1);

			bookShop.SellBook(book1.Guid);
			bookShop.Balance.Should().Be(0m);

			bookShop.SellBook(book2.Guid);
			bookShop.Balance.Should().Be(book2.Cost);
		}

		[Test]
		public void BookShopAddDaysTest()
		{
			BookShop.Core.BookShop bookShop = new BookShop.Core.BookShop(DateTime.Today.Date);

			bookShop.CurrentDate.Should().Be(DateTime.Today.Date);

			bookShop.AddDays(100);

			bookShop.CurrentDate.Should().Be(DateTime.Today.AddDays(100).Date);
		}

	}
}
