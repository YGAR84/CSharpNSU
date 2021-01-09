using BookShop.Core.Discounts;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Infrastructure.EntityFramework.Configurations.Discounts
{
	[UsedImplicitly]
	public class BookDiscountConfiguration : DiscountConfiguration<BookDiscount>
	{
		public override void Configure(EntityTypeBuilder<BookDiscount> builder)
		{
			builder.HasBaseType<Discount>();

			builder.HasOne(x => x.BookInfo)
				.WithMany(b => b.BookDiscounts)
				.HasForeignKey(x => x.BookInfoId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
