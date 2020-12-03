using System.Collections.Generic;
using BookShop.Core;
using FluentAssertions;
using NUnit.Framework;

namespace BookShop.Test
{
	[TestFixture]
	public class BookInfoTest
	{
		[Test]
		public void BookInfoCtorTest()
		{
			List<Genre> genres1 = new List<Genre> { new Genre("fantasy") };
			var title = "Harry Potter and the Chamber of Secrets";
			var author = "J.K.Rowling";
			BookInfo bookInfo1 = new BookInfo(title, author, genres1);

			bookInfo1.Title.Should().Be(title);
			bookInfo1.Author.Should().Be(author);
			bookInfo1.Genres.Should().BeEquivalentTo(genres1);
		}
	}
}