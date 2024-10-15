using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Aplication.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.APIs.Middlewares;

namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure Services

			webApplicationBuilder.Services
				.AddControllers()
				.ConfigureApiBehaviorOptions(options => 
				{ 
					options.SuppressModelStateInvalidFilter = false;
					options.InvalidModelStateResponseFactory = (actionContext) =>
					{
						var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
															.SelectMany(p => p.Value!.Errors)
															.Select(E => E.ErrorMessage);
						return new BadRequestObjectResult(new ApiValidationErrorResponse()
						{
							Errors = errors
						});

					};
				})
				.AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly); // register required services
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();

			webApplicationBuilder.Services.AddHttpContextAccessor();
			webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService),typeof(LoggedInUserServices));
			
			webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

			webApplicationBuilder.Services.AddApplicationServices();


			#endregion
			
			var app = webApplicationBuilder.Build();

			#region Databases Intialization

			await app.IntializeStoreContextAsync();

			#endregion

			// Configure the HTTP request pipeline.
			#region Configure Kestral Middlewares

			app.UseMiddleware<CustomExceptionHandlerMiddleware>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseStatusCodePagesWithReExecute("/Errors/{0}");
			app.UseAuthorization();

			app.UseStaticFiles();
			app.MapControllers();

			#endregion
			app.Run();
		}
	}
}
