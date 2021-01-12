using System;
using BookShop.Web.MassTransit;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BookShop.Web.Extensions
{
	internal static class RabbitMqServiceCollectionExtension
	{
		public static IServiceCollection AddRabbitMassTransit(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMassTransit(isp =>
				{
					var hostConfig = configuration.GetMassTransitConfiguration();

					return Bus.Factory.CreateUsingRabbitMq(cfg =>
					{
						var host = cfg.Host(
							new Uri(hostConfig.RabbitMqAddress),
							h =>
							{
								h.Username(hostConfig.UserName);
								h.Password(hostConfig.Password);
							});

						cfg.Durable = hostConfig.Durable;
						cfg.PurgeOnStartup = hostConfig.PurgeOnStartup;

						cfg.ReceiveEndpoint(host,
							Constants.ResponseQueue, ep =>
							{
								ep.PrefetchCount = 1;
								ep.ConfigureConsumer<BooksReceivedConsumer>(isp);
							});
					});
				},
				ispc =>
				{
					ispc.AddConsumers(typeof(BooksReceivedConsumer).Assembly);
				});

			return services;
		}
    }
}
