using System;
using BookShop.Core.Discounts;
using BookShop.Logic.Responses.DiscountsResponses;

namespace BookShop.Logic.Utils.GetDiscountResponseBuilder
{
	public class GetDefectDiscountResponseBuilder : GetDiscountResponseBuilder
	{
		private Guid _bookGuid;

		public GetDefectDiscountResponseBuilder SetBookGuid(Guid bookGuid)
		{
			_bookGuid = bookGuid;
			return this;
		}

		public override GetDiscountResponse GetDiscountResponse()
		{
			return new GetDefectDiscountResponse()
			{
				BookGuid = _bookGuid,
				DiscountPercentage = DiscountPercentage,
				ExpireDate = ExpireDate,
				Id = Id
			};
		}

		public static implicit operator GetDiscountResponse(GetDefectDiscountResponseBuilder builder)
		{
			return builder.GetDiscountResponse();
		}
	}
}