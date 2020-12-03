using System;
using System.Collections.Generic;
using System.Text;
using BookShop.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JetBrains.Annotations;


namespace BookShop.Infrastructure.EntityFramework.Configurations
{

	[UsedImplicitly]
	public class BookInfoConfiguration : IEntityTypeConfiguration<BookInfo>
	{
		public void Configure(EntityTypeBuilder<BookInfo> builder)
		{
			builder.ToTable(nameof(BookInfo), BookShopContext.DefaultSchemaName);
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.Title).HasMaxLength(100);
			builder.Property(x => x.Author).HasMaxLength(100);

			builder.HasMany(x => x.Books)
				.WithOne(b => b.BookInfo)
				.HasForeignKey(b => b.BookInfoId)
				.OnDelete(DeleteBehavior.SetNull);

		}
	}
}
