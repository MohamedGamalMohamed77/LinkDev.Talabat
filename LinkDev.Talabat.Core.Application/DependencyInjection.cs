using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			//services.AddAutoMapper(mapper => mapper.AddProfile(new MappingProfile()));
			services.AddAutoMapper(typeof(MappingProfile));

			//services.AddScoped(typeof(IProductService), typeof(ProductService));
			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

			services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
			{
				var mapper = serviceProvider.GetRequiredService<IMapper>();
				var configuration = serviceProvider.GetRequiredService<IConfiguration>();
				var basketRepository = serviceProvider.GetRequiredService<IBasketRepository>();

				return new BasketService(basketRepository, mapper, configuration);
			});

			return services;
		}

	}
}
