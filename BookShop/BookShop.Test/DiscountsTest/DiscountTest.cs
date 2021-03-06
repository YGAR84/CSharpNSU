﻿using System;
using BookShop.Core;
using BookShop.Core.Discounts;
using FluentAssertions;
using NUnit.Framework;

namespace BookShop.Test.DiscountsTest
{
	public class DiscountTest
	{
		[Test]
		public void DiscountIsExpiredTest()
		{
			Discount discount = new GenreDiscount(DateTime.Today.AddDays(1000).Date, 10, new Genre("bl"));

			discount.IsExpired(DateTime.Today.AddDays(10001).Date).Should().BeTrue();
			discount.IsExpired(DateTime.Today.Date).Should().BeFalse();
			discount.IsExpired(DateTime.Today.AddDays(1000).Date).Should().BeFalse();
		}

	}
}