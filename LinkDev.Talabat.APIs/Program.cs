using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Aplication.Abstraction;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
															.Select(p => new ApiValidationErrorResponse.ValidationError()
															{
																Field = p.Key,
																Errors = p.Value!.Errors.Select(E => E.ErrorMessage)
															});
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


			webApplicationBuilder.Services.AddApplicationServices();
			webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);
			webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);


			webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>((idenityOptions)=>
				{
					idenityOptions.SignIn.RequireConfirmedAccount = true;
					idenityOptions.SignIn.RequireConfirmedEmail = true;
					idenityOptions.SignIn.RequireConfirmedPhoneNumber = true;

					idenityOptions.Password.RequireNonAlphanumeric = true;
					idenityOptions.Password.RequiredUniqueChars = 2;
					idenityOptions.Password.RequiredLength = 6;
					idenityOptions.Password.RequireDigit = true;
					idenityOptions.Password.RequireUppercase = true;
					idenityOptions.Password.RequireLowercase = true;

					idenityOptions.User.RequireUniqueEmail = true;

					idenityOptions.Lockout.AllowedForNewUsers = true;
					idenityOptions.Lockout.MaxFailedAccessAttempts = 5;
					idenityOptions.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromHours(12);

				})
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			#endregion

			var app = webApplicationBuilder.Build();

			#region Databases Intialization

			await app.IntializeDbAsync();

			#endregion

			// Configure the HTTP request pipeline.
			#region Configure Kestral Middlewares

			app.UseMiddleware<ExceptionHandlerMiddleware>();

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
