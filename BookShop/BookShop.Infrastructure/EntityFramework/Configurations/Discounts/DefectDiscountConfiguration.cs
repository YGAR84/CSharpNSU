﻿using System;
using System.Collections.Generic;
using System.Text;
using BookShop.Core.Discounts;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Infrastructure.EntityFramework.Configurations.Discounts
{


	[UsedImplicitly]
	public class DefectDiscountConfiguration : DiscountConfiguration<DefectDiscount>
	{
		public override void Configure(EntityTypeBuilder<DefectDiscount> builder)
		{
			base.Configure(builder);

			builder.HasOne(x => x.Book)
				.WithMany(b => b.DefectDiscounts)
				.HasForeignKey(x => x.BookGuid)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
