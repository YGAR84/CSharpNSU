using BookShop.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JetBrains.Annotations;


namespace BookShop.Infrastructure.EntityFramework.Configurations
{
	[UsedImplicitly]
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable(nameof(Book), BookShopContext.DefaultSchemaName);
			builder.HasKey(x => x.Guid);
			builder.Property(x => x.Guid).ValueGeneratedOnAdd();

			builder.HasMany(x => x.DefectDiscounts)
				.WithOne(d => d.Book)
				.HasForeignKey(b => b.BookGuid);
		}
	}
}
