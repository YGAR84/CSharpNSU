using System;
using System.Net.Http;
using BookShop.Core;
using BookShop.Infrastructure.EntityFramework;
using BookShop.Integration.ExternalServices;
using BookShop.Logic;
using BookShop.Web.Extensions;
using BookShop.Web.MassTransit;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookShop.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddNewtonsoftJson(
					options =>
					{
						options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
						options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
						options.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
						options.SerializerSettings.ContractResolver = new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() };
					});

			services.AddRabbitMassTransit(Configuration);

			services.AddSingleton<HttpClient>();
			services.AddScoped<IBookServiceProxy, ServiceProxy>();
			services.AddSingleton(isp => new BookShopContextDbContextFactory(Configuration.GetConnectionString("DefaultConnection")));
			services.AddSingleton(s => 
				new BookShopService(s.GetRequiredService<BookShopContextDbContextFactory>(), 1));

			services.AddBackgroundJobs();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapControllers());

			app.UseHttpsRedirection();
			
		}
	}
}
