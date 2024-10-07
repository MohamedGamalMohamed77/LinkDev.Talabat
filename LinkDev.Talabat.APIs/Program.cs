using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure Services
			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();

			
			webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

			#endregion
			
			var app = webApplicationBuilder.Build();
			
			#region Update-Database
			
			var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;
			var dbcontext = services.GetRequiredService<StoreContext>();

			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				var pindingMigrations = dbcontext.Database.GetPendingMigrations();

				if (pindingMigrations.Any())
					await dbcontext.Database.MigrateAsync();//Update-Database

				await StoreContextSeed.SeedAsync(dbcontext);
			}
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "an error has been occured during applaying migrations");
			} 
			#endregion

			// Configure the HTTP request pipeline.
			#region Configure Kestral Middlewares

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			#endregion
			app.Run();
		}
	}
}
