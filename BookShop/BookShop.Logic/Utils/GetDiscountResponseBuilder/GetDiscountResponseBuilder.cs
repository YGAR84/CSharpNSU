using System;
using BookShop.Logic.Responses.DiscountsResponses;

namespace BookShop.Logic.Utils.GetDiscountResponseBuilder
{
	public abstract class GetDiscountResponseBuilder
	{
		protected int Id;
		protected decimal DiscountPercentage;
		protected DateTime ExpireDate;

		public GetDiscountResponseBuilder SetExpireDate(DateTime expireDate)
		{
			ExpireDate = expireDate;
			return this;
		}

		public GetDiscountResponseBuilder SetDiscountPercentage(decimal discountPercentage)
		{
			DiscountPercentage = discountPercentage;
			return this;
		}

		public GetDiscountResponseBuilder SetId(int id)
		{
			Id = id;
			return this;
		}

		public abstract GetDiscountResponse GetDiscountResponse();
	}
}