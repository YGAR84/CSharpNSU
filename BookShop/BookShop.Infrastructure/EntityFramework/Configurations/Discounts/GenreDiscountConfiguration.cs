using System;
using System.Collections.Generic;
using System.Text;
using BookShop.Core.Discounts;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Infrastructure.EntityFramework.Configurations.Discounts
{

	[UsedImplicitly]
	public class GenreDiscountConfiguration : DiscountConfiguration<GenreDiscount> 
	{
		public override void Configure(EntityTypeBuilder<GenreDiscount> builder)
		{
			//base.Configure(builder);

			builder.HasBaseType<Discount>();

			builder.HasOne(x => x.Genre)
				.WithMany(b => b.GenreDiscounts)
				.HasForeignKey(x => x.GenreId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
