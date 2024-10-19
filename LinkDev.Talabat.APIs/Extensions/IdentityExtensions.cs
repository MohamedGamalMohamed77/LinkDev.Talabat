using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.APIs.Extensions
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));

			services.AddIdentity<ApplicationUser, IdentityRole>((idenityOptions) =>
			{
				idenityOptions.SignIn.RequireConfirmedAccount = true;
				idenityOptions.SignIn.RequireConfirmedEmail = true;
				idenityOptions.SignIn.RequireConfirmedPhoneNumber = true;

				//idenityOptions.Password.RequireNonAlphanumeric = true;
				//idenityOptions.Password.RequiredUniqueChars = 2;
				//idenityOptions.Password.RequiredLength = 6;
				//idenityOptions.Password.RequireDigit = true;
				//idenityOptions.Password.RequireUppercase = true;
				//idenityOptions.Password.RequireLowercase = true;

				idenityOptions.User.RequireUniqueEmail = true;

				idenityOptions.Lockout.AllowedForNewUsers = true;
				idenityOptions.Lockout.MaxFailedAccessAttempts = 5;
				idenityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

			})
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			return services;
		}

	}
}
