using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbIntializers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence._Common
{
	public abstract class DbIntializer(DbContext _dbContext) : IDbIntializer
	{
		public virtual  async Task IntializeAsync()
		{
			var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

			if (pendingMigrations.Any())
				await _dbContext.Database.MigrateAsync(); // Update-Database
		}

		public  abstract Task SeedAsync();
	}
}
