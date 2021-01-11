using BookShop.BooksProvider.MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookShop.BooksProvider.Extensions
{
	internal static class ConfigurationGetMasTransitExtension
	{
		private const string MassTransit = "MassTransit";

		public static MassTransitConfiguration GetMassTransitConfiguration(this IConfiguration configuration)
		{
			#warning можно так сделать, да, а можно ещё красивее 
			/*
			 * services.Configure<MassTransitConfiguration>(configuration.GetSection("MassTransit"));
			   services.AddSingleton<IMassTransitConfiguration>(isp => isp.GetRequiredService<IOptions<MassTransitConfiguration>>().Value);
			 * 
			 */
			#warning т.е. просто зарегать в DI IMassTransitConfiguration, и инжектить куда надо. 
			#warning но, пожалуй, в этом случае это был бы оверинжиниринг
			var hostConfig = new MassTransitConfiguration();
			configuration.GetSection(MassTransit).Bind(hostConfig);

			return hostConfig;
		}
	}
}
