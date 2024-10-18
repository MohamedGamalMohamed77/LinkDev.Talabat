using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbIntializers;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
	public class StoreIdentityDbIntializer(StoreIdentityDbContext _dbContext,UserManager<ApplicationUser> _userManager) : DbIntializer(_dbContext),IStoreIdentityDbIntializer
	{
		public override async Task SeedAsync()
		{
			var user = new ApplicationUser()
			{
				DisplayName = "Mohamed Gamal",
				UserName = "mohamed.gamal",
				Email = "mohamed.gamal@voice.com",
				PhoneNumber = "01122334455"
			};
			
			await _userManager.CreateAsync(user,"P@ssword");
		}

	}
}
