using LinkDev.Talabat.Core.Domain.Contracts.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependecyInjection 
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection Services, IConfiguration Configuration)
		{
			Services.AddDbContext<StoreContext>((options) =>
			{
				options
				.UseLazyLoadingProxies()
				.UseSqlServer(Configuration.GetConnectionString("StoreContext"));
			});

			
			Services.AddScoped(typeof(IStoreContextIntializer), typeof(StoreContextIntializer));
			Services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
			Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

			return Services;
		}
	}
}
