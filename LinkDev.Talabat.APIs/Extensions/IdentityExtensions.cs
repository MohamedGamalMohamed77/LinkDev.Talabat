using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkDev.Talabat.APIs.Extensions
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));

			services.AddIdentity<ApplicationUser, IdentityRole>((idenityOptions) =>
			{
				#region Sign In
				//idenityOptions.SignIn.RequireConfirmedAccount = true;
				//idenityOptions.SignIn.RequireConfirmedEmail = true;
				//idenityOptions.SignIn.RequireConfirmedPhoneNumber = true;

				#endregion

				#region Password
				//idenityOptions.Password.RequireNonAlphanumeric = true;
				//idenityOptions.Password.RequiredUniqueChars = 2;
				//idenityOptions.Password.RequiredLength = 6;
				//idenityOptions.Password.RequireDigit = true;
				//idenityOptions.Password.RequireUppercase = true;
				//idenityOptions.Password.RequireLowercase = true;

				#endregion

				idenityOptions.User.RequireUniqueEmail = true;

				idenityOptions.Lockout.AllowedForNewUsers = true;
				idenityOptions.Lockout.MaxFailedAccessAttempts = 5;
				idenityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

			})
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			services.AddAuthentication((authenticationOptions) =>
			{
				authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer((options) =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = configuration["JwtSettings:Issuer"],
					ValidAudience = configuration["JwtSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
					ClockSkew = TimeSpan.Zero

				};

			});


			services.AddScoped(typeof(IAuthService), typeof(AuthService));
			services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
			{
				return () => serviceProvider.GetRequiredService<IAuthService>();

			});

			return services;
		}

	}
}
