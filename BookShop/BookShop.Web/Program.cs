using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BookShop.Web
{
	public class Program
	{
		#warning в целом тоже норм, но я уже слишком вялый, не до конца понял все твои задумки, что у тебя там со скидками в базе происходит.
		#warning тут в основном замечания такие же - мусор в коде, почисти. 
		#warning всё равно ещё раз смотреть, потом посмотрю что у тебя там со скидками. 
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
