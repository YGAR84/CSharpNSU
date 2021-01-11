using BookShop.Core;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Infrastructure.EntityFramework.Configurations
{
	[UsedImplicitly]
	public class BookShopStateConfiguration : IEntityTypeConfiguration<BookShopState>
	{
		private const int DefaultStorageSize = 100;
		private const decimal DefaultBalance = 100000M;

		public void Configure(EntityTypeBuilder<BookShopState> builder)
		{
			builder.ToTable(nameof(BookShopState), BookShopContext.DefaultSchemaName);
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.Balance).IsRequired().HasDefaultValue(DefaultBalance);

			builder.Property(x => x.StorageSize).IsRequired().HasDefaultValue(DefaultStorageSize);
			#warning разве не должно быть связи со всеми книгами, которые есть в магазине? 
	
		}
	}
}