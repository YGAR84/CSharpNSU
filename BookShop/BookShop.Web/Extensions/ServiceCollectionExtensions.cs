﻿using System.Collections.Specialized;
using BookShop.Web.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace BookShop.Web.Extensions
{
	internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, InjectableJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>(isp =>
            {
	            var properties = new NameValueCollection
                {
                    ["quartz.scheduler.interruptJobsOnShutdownWithWait"] = "true",
                    ["quartz.scheduler.interruptJobsOnShutdown"] = "true"
                };
                return new StdSchedulerFactory(properties);
            });
            services.AddSingleton<BooksCountCheckJob>();
            services.AddHostedService<QuartzHostedService>();
            return services;
        }
    }
}
