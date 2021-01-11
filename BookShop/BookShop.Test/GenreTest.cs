using BookShop.Core;
using FluentAssertions;
using NUnit.Framework;

namespace BookShop.Test
{
	[TestFixture]
	public class GenreTest
	{
		[Test]
		public void GenreCtorTest()
		{
			var genreName = "fantasy";
			Genre genre = new Genre(genreName);

			genre.Name.Should().Be(genreName);
		}

		[Test]
		public void GenreEqualsTest()
		{
			var genreName = "fantasy";
			Genre genre1 = new Genre(genreName);
			Genre genre2 = new Genre(genreName);
			Genre genre3 = new Genre("lalala");

			genre1.Equals(genre1).Should().BeTrue();
			genre1.Equals(genre2).Should().BeTrue();
			genre1.Equals(genre3).Should().BeFalse();

			genre1.Equals(null).Should().BeFalse();
			genre1.Equals(null as Genre).Should().BeFalse();

			genre1.Equals(genre2 as object).Should().BeTrue();
			genre1.Equals(genre3 as object).Should().BeFalse();

			genre1.Equals(genre1 as object).Should().BeTrue();
			genre1.Equals(null as object).Should().BeFalse();

			genre1.Equals(new object()).Should().BeFalse();
		}

		[Test]
		public void GenreOperatorEqualsTest()
		{
			var genreName = "fantasy";
			Genre genre1 = new Genre(genreName);
			Genre genre2 = new Genre(genreName);
			Genre genre3 = new Genre("lalala");
			Genre genre4 = null;

			Assert.IsTrue(genre1 == genre2);
			Assert.IsFalse(genre1 == genre3);

			Assert.IsFalse(genre1 == null);
			Assert.IsFalse(null as Genre == genre1);
			Assert.IsTrue(genre4 == null as Genre);
		}

		[Test]
		public void GenreOperatorInEqualsTest()
		{
			var genreName = "fantasy";
			Genre genre1 = new Genre(genreName);
			Genre genre2 = new Genre(genreName);
			Genre genre3 = new Genre("lalala");
			Genre genre4 = null;

			Assert.IsFalse(genre1 != genre2);
			Assert.IsTrue(genre1 != genre3);

			Assert.IsTrue(genre1 != null);
			Assert.IsTrue(null as Genre != genre1);
			Assert.IsFalse(genre4 != null as Genre);
		}

		[Test]
		public void GenreHashCodeTest()
		{
			var genreName = "fantasy";
			Genre genre1 = new Genre(genreName);
			Genre genre2 = new Genre(null);

			genre1.GetHashCode().Should().Be(genreName.GetHashCode());
			genre2.GetHashCode().Should().Be(0);
		}
	}
}