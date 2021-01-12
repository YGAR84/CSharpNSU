using System;

namespace BookShop.Core.Discounts
{
	public class DefectDiscount : Discount
	{
		public Guid BookGuid { get; set; }

		public Book Book { get; set; }

		public DefectDiscount() {}
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