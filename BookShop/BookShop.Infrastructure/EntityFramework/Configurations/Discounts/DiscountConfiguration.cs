using BookShop.Core.Discounts;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Infrastructure.EntityFramework.Configurations.Discounts
{
#warning Основная идея - сделать TPH для скидочек
#warning Нашёл в инете как это делают руками, потому что до этого Fluent юзал
#warning Когда делаю TPH, то могу применять скидки к книге независимо от типа скидки, что в принципе удобно
#warning не знаю, как такое в нормальных проектах делают)

	[UsedImplicitly]
	public abstract class DiscountConfiguration<TBase> : IEntityTypeConfiguration<TBase>
		where TBase : Discount
	{
		public virtual void Configure(EntityTypeBuilder<TBase> builder)
		{
			builder.HasDiscriminator<string>("Discriminator")
				.HasValue<BookDiscount>(nameof(BookDiscount))
				.HasValue<DefectDiscount>(nameof(DefectDiscount))
				.HasValue<GenreDiscount>(nameof(GenreDiscount));

			builder.ToTable(nameof(Discount), BookShopContext.DefaultSchemaName);
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.DiscountPercentage).IsRequired();
			builder.Property(x => x.ExpireDate).IsRequired();
		}
	}
}
