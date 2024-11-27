using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Aplication.Abstraction.Services.Auth
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto model);
		Task<UserDto> RegisterAsync(RegisterDto model);
		Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

	}
}
