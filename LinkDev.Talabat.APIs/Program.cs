using LinkDev.Talabat.APIs.Extensions;
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

			#region Databases Intialization

			await app.IntializeStoreContextAsync();
			
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
