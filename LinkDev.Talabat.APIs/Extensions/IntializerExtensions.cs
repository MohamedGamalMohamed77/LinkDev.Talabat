using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbIntializers;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using System.Runtime.CompilerServices;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IntializerExtensions
	{
		public static async Task<WebApplication> IntializeDbAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;
			
			var storeContextIntializer = services.GetRequiredService<IStoreDbIntializer>();
			var storeIdentityContextIntializer = services.GetRequiredService<IStoreIdentityDbIntializer>();

			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				await storeContextIntializer.IntializeAsync();
				await storeContextIntializer.SeedAsync();

				await storeIdentityContextIntializer.IntializeAsync();
				await storeIdentityContextIntializer.SeedAsync();

			}
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "an error has been occured during applaying migrations");
			}

			return app;

		}
		
	}
}
