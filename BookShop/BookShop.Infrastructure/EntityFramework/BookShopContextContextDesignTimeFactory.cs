using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookShop.Infrastructure.EntityFramework
{
	[UsedImplicitly]
	public sealed class BookShopContextContextDesignTimeFactory : IDesignTimeDbContextFactory<BookShopContext>
	{
		private const string DefaultConnectionString =
			"Server=localhost\\SQLEXPRESS;Database=BookShop;Trusted_Connection=True;";

		public static DbContextOptions<BookShopContext> GetSqlServerOptions([CanBeNull] string connectionString)
		{
			return new DbContextOptionsBuilder<BookShopContext>()
				.UseSqlServer(connectionString ?? DefaultConnectionString, x =>
				{
					x.MigrationsHistoryTable("__EFMigrationsHistory", BookShopContext.DefaultSchemaName);
				})
				.Options;
		}
		public BookShopContext CreateDbContext(string[] args)
		{
			return new BookShopContext(GetSqlServerOptions(null));
		}
	}
}
