using BookShop.BooksProvider.MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookShop.BooksProvider.Extensions
{
	internal static class ConfigurationGetMasTransitExtension
	{
		private const string MassTransit = "MassTransit";

		public static MassTransitConfiguration GetMassTransitConfiguration(this IConfiguration configuration)
		{
			var hostConfig = new MassTransitConfiguration();
			configuration.GetSection(MassTransit).Bind(hostConfig);

			return hostConfig;
		}
	}
}
