using LinkDev.Talabat.Core.Domain.Contracts.Products;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependecyInjection 
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection Services, IConfiguration Configuration)
		{
			#region Store Context
			Services.AddDbContext<StoreDbContext>((options) =>
				{
					options
					.UseLazyLoadingProxies()
					.UseSqlServer(Configuration.GetConnectionString("StoreContext"));
				});


			Services.AddScoped(typeof(IStoreContextIntializer), typeof(StoreDbContextIntializer));
			Services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

			#endregion

			#region IdentityDbContext
			Services.AddDbContext<StoreIdentityDbContext>((options) =>
			{
				options
				.UseLazyLoadingProxies()
				.UseSqlServer(Configuration.GetConnectionString("IdentityContext"));
			});
			#endregion


			Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

			return Services;
		}
	}
}
