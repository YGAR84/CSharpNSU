using System;
using System.Collections.Generic;
using BookShop.Core.Discounts;

namespace BookShop.Core
{
	public class Genre : IEquatable<Genre>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<BookInfo> BookInfos { get; set; }
		public List<GenreDiscount> GenreDiscounts { get; set; }

		public Genre(string name)
		{
			Name = name;
		}


		public static bool operator ==(Genre g1, Genre g2)
		{
			if (g1 is null && g2 is null) return true;
			if (g1 is null) return false;

			return g1.Equals(g2);
		}

		public static bool operator !=(Genre g1, Genre g2)
		{
			return !(g1 == g2);
		}

		public bool Equals(Genre other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Name == other.Name;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Genre)obj);
		}

		public override int GetHashCode()
		{
			return (Name != null ? Name.GetHashCode() : 0);
		}
	}
}