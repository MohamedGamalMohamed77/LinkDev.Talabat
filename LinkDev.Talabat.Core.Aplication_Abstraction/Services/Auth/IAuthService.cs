using LinkDev.Talabat.Core.Aplication.Abstraction.Common;
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
		Task<UserDto> LoginAsync(LoginDto loginDto);
		Task<UserDto> RegisterAsync(RegisterDto registerDto);
		Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
		Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal);
		Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal,AddressDto addressDto);

		Task<bool> EmailExists(string email);

	}
}
