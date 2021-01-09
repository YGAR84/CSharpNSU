using BookShop.Logic.Responses.DiscountsResponses;

namespace BookShop.Logic.Utils.GetDiscountResponseBuilder
{
	public class GetGenreDiscountResponseBuilder : GetDiscountResponseBuilder
	{
		private string _genreName;

		public GetGenreDiscountResponseBuilder SetGenreName(string genreName)
		{
			_genreName = genreName;
			return this;
		}

		public override GetDiscountResponse GetDiscountResponse()
		{
			return new GetGenreDiscountResponse()
			{
				GenreName = _genreName,
				DiscountPercentage = DiscountPercentage,
				ExpireDate = ExpireDate,
				Id = Id
			};
		}

		public static implicit operator GetDiscountResponse(GetGenreDiscountResponseBuilder builder)
		{
			return builder.GetDiscountResponse();
		}
	}
}