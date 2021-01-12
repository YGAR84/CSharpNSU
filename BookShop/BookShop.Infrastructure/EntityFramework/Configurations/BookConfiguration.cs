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

			builder.Property(x => x.Cost).IsRequired();

#warning честно говоря, такой вариант мне не очень нравится, но что-то через Intellisence я не нашёл, как сделать красиво :) 
#warning мне тоже, но эти конфигураторы так не умеют, в отличии от аннотаций, в которые можно прописывать чеки по типа [Range(0, INT_MAX)] около пропса
			builder.HasCheckConstraint("CK_BookShop.Book_Cost", "[Cost] > 0");

			builder.Property(x => x.ArriveDate).IsRequired();

			builder.HasOne(x => x.BookInfo)
				.WithMany(x => x.Books)
				.HasForeignKey(x => x.BookInfoId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(x => x.DefectDiscounts)
				.WithOne(d => d.Book)
				.HasForeignKey(b => b.BookGuid)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
