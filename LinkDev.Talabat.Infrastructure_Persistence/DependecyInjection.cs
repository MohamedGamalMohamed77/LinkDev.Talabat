using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
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
				options.UseSqlServer(Configuration.GetConnectionString("StoreContext"));
			});

			Services.AddScoped(typeof(IStoreContextIntializer), typeof(StoreContextIntializer));

			return Services;
		}
	}
}
