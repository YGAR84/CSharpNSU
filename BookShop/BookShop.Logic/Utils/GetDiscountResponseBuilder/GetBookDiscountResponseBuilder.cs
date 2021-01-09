using BookShop.Logic.Responses.DiscountsResponses;

namespace BookShop.Logic.Utils.GetDiscountResponseBuilder
{
	public class GetBookDiscountResponseBuilder : GetDiscountResponseBuilder
	{
		private int _bookInfoId;

		public GetBookDiscountResponseBuilder SetBookInfoId(int bookInfoId)
		{
			_bookInfoId = bookInfoId;
			return this;
		}

		public override GetDiscountResponse GetDiscountResponse()
		{
			return new GetBookDiscountResponse()
			{
				BookInfoId = _bookInfoId,
				DiscountPercentage = DiscountPercentage,
				ExpireDate = ExpireDate,
				Id = Id
			};
		}

		public static implicit operator GetDiscountResponse(GetBookDiscountResponseBuilder builder)
		{
			return builder.GetDiscountResponse();
		}
	}
}