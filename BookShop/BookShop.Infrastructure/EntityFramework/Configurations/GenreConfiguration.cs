using BookShop.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JetBrains.Annotations;

namespace BookShop.Infrastructure.EntityFramework.Configurations
{
	[UsedImplicitly]
	public class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder.ToTable(nameof(Genre), BookShopContext.DefaultSchemaName);
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

			builder.HasIndex(g => g.Name).IsUnique();

			builder.HasMany(x => x.GenreDiscounts)
				.WithOne(x => x.Genre)
				.HasForeignKey(x => x.GenreId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasMany(x => x.BookInfos)
				.WithMany(x => x.Genres);

		}
	}
}