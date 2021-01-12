using System;
using BookShop.BooksProvider.MassTransit;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.BooksProvider.Extensions
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
							hostConfig.ResponseQueue, ep =>
							{
								ep.PrefetchCount = 1;
								ep.ConfigureConsumer<BooksReceivedRequestConsumer>(isp);
							});
					});
				},
				ispc =>
				{
					ispc.AddConsumers(typeof(BooksReceivedRequestConsumer).Assembly);
				});

			return services;
		}
    }
}
