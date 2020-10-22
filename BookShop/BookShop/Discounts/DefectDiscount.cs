using System;

namespace BookShop.Discounts
{
	public class DefectDiscount : Discount
	{
		public Guid BookGuid { get; }

		public DefectDiscount(DateTime expireDate, int discountPercentage, Guid bookGuid) : base(expireDate, discountPercentage)
		{
			BookGuid = bookGuid;
		}

		protected override bool HasDiscount(Book book)
		{
			return book.Guid == BookGuid;
		}
	}
}