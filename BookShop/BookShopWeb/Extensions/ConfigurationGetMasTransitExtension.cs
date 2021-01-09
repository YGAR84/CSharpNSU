using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Web.MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookShop.Web.Extensions
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
