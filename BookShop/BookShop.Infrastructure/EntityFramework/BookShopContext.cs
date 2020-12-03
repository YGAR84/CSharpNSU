using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Core;
using BookShop.Core.Discounts;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infrastructure.EntityFramework
{
	public class BookShopContext : DbContext
	{
		public const string DefaultSchemaName = "BookShop";

		private DbSet<Genre> _genre;
		private DbSet<BookInfo> _bookInfos;
		private DbSet<Book> _books;

		private DbSet<Discount> _discounts;
		private DbSet<DefectDiscount> _defectDiscounts;
		private DbSet<BookDiscount> _bookDiscounts;
		private DbSet<GenreDiscount> _genreDiscounts;

		public BookShopContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
			modelBuilder.HasDefaultSchema(DefaultSchemaName);
		}

		public async Task<List<Book>> GetBooks()
		{
			return await _books
				.Include(b => b.BookInfo)
				.Include(b => b.DefectDiscounts)
				.ToListAsync();
		}
	}
}
