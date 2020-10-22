using System;
using System.Collections.Generic;
using BookShop;
using NUnit.Framework;
using FluentAssertions;

namespace BookShopTest
{
	[TestFixture]
	public class BookStorageTest
	{
		[Test]
		public void BookStorageAddBookTest()
		{
			IReadOnlyList<Genre> genres = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres);
			Book book = new Book(bookInfo, 500m, DateTime.Today);

			BookStorage bookStorage = new BookStorage();

			bookStorage.AddBook(book);

			bookStorage.FindByGuidOrDefault(book.Guid).Should().Be(book);
		}

		[Test]
		public void BookStorageAddTwoBooksTest()
		{
			IReadOnlyList<Genre> genres = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres);
			Book book1 = new Book(bookInfo, 500m, DateTime.Today);
			Book book2 = new Book(bookInfo, 600m, DateTime.Today.AddDays(-110));

			BookStorage bookStorage = new BookStorage();

			bookStorage.AddBook(book1);
			bookStorage.AddBook(book2);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(book1);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);
		}

		[Test]
		public void BookStorageAddTwoDifferentBooksTest()
		{
			IReadOnlyList<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			Book book1 = new Book(bookInfo1, 500m, DateTime.Today);

			IReadOnlyList<Genre> genres2 = new List<Genre> { new Genre("other") };
			BookInfo bookInfo2 = new BookInfo("One Flew Over the Cuckoo's Nest", " Ken Kesey", genres2);
			Book book2 = new Book(bookInfo2, 600m, DateTime.Today.AddDays(-110));


			BookStorage bookStorage = new BookStorage();

			bookStorage.AddBook(book1);
			bookStorage.AddBook(book2);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(book1);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);
		}

		[Test]
		public void BookStorageDeleteBookTest()
		{
			IReadOnlyList<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			Book book1 = new Book(bookInfo1, 500m, DateTime.Today);

			IReadOnlyList<Genre> genres2 = new List<Genre> { new Genre("other") };
			BookInfo bookInfo2 = new BookInfo("One Flew Over the Cuckoo's Nest", " Ken Kesey", genres2);
			Book book2 = new Book(bookInfo2, 600m, DateTime.Today.AddDays(-110));


			BookStorage bookStorage = new BookStorage();

			bookStorage.AddBook(book1);
			bookStorage.AddBook(book2);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(book1);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);

			bookStorage.DeleteBook(book1);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(null);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);
		}

		[Test]
		public void BookStorageDeleteNotExistingBookTest()
		{
			IReadOnlyList<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			Book book1 = new Book(bookInfo1, 500m, DateTime.Today);

			IReadOnlyList<Genre> genres2 = new List<Genre> { new Genre("other") };
			BookInfo bookInfo2 = new BookInfo("One Flew Over the Cuckoo's Nest", " Ken Kesey", genres2);
			Book book2 = new Book(bookInfo2, 600m, DateTime.Today.AddDays(-110));


			BookStorage bookStorage = new BookStorage();

			bookStorage.AddBook(book2);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(null);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);

			bookStorage.DeleteBook(book1);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(null);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);
		}

		[Test]
		public void BookStorageDeleteBookByGuidTest()
		{
			IReadOnlyList<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			Book book1 = new Book(bookInfo1, 500m, DateTime.Today);

			IReadOnlyList<Genre> genres2 = new List<Genre> { new Genre("other") };
			BookInfo bookInfo2 = new BookInfo("One Flew Over the Cuckoo's Nest", " Ken Kesey", genres2);
			Book book2 = new Book(bookInfo2, 600m, DateTime.Today.AddDays(-110));


			BookStorage bookStorage = new BookStorage();

			bookStorage.AddBook(book1);
			bookStorage.AddBook(book2);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(book1);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);

			bookStorage.DeleteBook(book1.Guid);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(null);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);
		}

		[Test]
		public void BookStorageDeleteNotExistingBookByGuidTest()
		{
			IReadOnlyList<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			BookInfo bookInfo1 = new BookInfo("Harry Potter and the Chamber of Secrets", "J.K.Rowling", genres1);
			Book book1 = new Book(bookInfo1, 500m, DateTime.Today);

			IReadOnlyList<Genre> genres2 = new List<Genre> { new Genre("other") };
			BookInfo bookInfo2 = new BookInfo("One Flew Over the Cuckoo's Nest", " Ken Kesey", genres2);
			Book book2 = new Book(bookInfo2, 600m, DateTime.Today.AddDays(-110));


			BookStorage bookStorage = new BookStorage();

			bookStorage.AddBook(book2);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(null);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);

			bookStorage.DeleteBook(book1.Guid);

			bookStorage.FindByGuidOrDefault(book1.Guid).Should().Be(null);
			bookStorage.FindByGuidOrDefault(book2.Guid).Should().Be(book2);
		}
	}

}