namespace BookShop.Infrastructure
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var bshp = new EntityFramework.BookShopContextDbContextFactory(null).GetContext();
			var sa = bshp.GetBooks();
		}
	}
}
