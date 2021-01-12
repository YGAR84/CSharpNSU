using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookShop.BooksProvider
{
	public class Program
	{
		#warning в целом, в этой части всё довольно-таки хорошо. пошёл вторую часть смотреть :) 
		#warning а, ну appsettings.Development.json можно было и удалить, он же тоже не нужен
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
