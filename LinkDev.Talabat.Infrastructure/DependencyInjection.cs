using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Infrastructure.Payment_Service;
using LinkDev.Talabat.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
			{
				var connectionString = configuration.GetConnectionString("Redis");
				var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString!);
				return connectionMultiplexer;
			});

			services.AddScoped(typeof(IBasketRepository),typeof(Basket_Repository.BasketRepository));
			
			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
			services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
			services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));
			return services;

			
		}
	}
}
