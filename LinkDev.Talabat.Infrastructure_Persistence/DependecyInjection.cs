using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbIntializers;
using LinkDev.Talabat.Core.Domain.Contracts.Products;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependecyInjection 
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration Configuration)
		{
			#region Store Context
			services.AddDbContext<StoreDbContext>((serviceProvider,options) =>
				{
					options
					.UseLazyLoadingProxies()
					.UseSqlServer(Configuration.GetConnectionString("StoreContext"))
					.AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());
				});


			services.AddScoped(typeof(IStoreDbIntializer), typeof(StoreDbIntializer));
			services.AddScoped(typeof(AuditInterceptor));
			//services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

			#endregion

			#region IdentityDbContext
			services.AddDbContext<StoreIdentityDbContext>((options) =>
			{
				options
				.UseLazyLoadingProxies()
				.UseSqlServer(Configuration.GetConnectionString("IdentityContext"));
			});
			services.AddScoped(typeof(IStoreIdentityDbIntializer), typeof(StoreIdentityDbIntializer));


			#endregion

			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));


			return services;
		}
	}
}
