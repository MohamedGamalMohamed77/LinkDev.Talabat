using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public class StoreContextIntializer(StoreContext _dbContext) : IStoreContextIntializer
	{
		public async Task IntializeAsync()
		{
			var pindingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

			if (pindingMigrations.Any())
				await _dbContext.Database.MigrateAsync();
		}

		public async Task SeedAsync()
		{
			if (!_dbContext.Brands.Any())
			{
				var brandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);


				if (brands?.Count > 0)
				{
					await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
					await _dbContext.SaveChangesAsync();
				}
			}

			if (!_dbContext.Categories.Any())
			{
				var categoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);


				if (categories?.Count > 0)
				{
					await _dbContext.Set<ProductCategory>().AddRangeAsync(categories);
					await _dbContext.SaveChangesAsync();
				}
			}

			if (!_dbContext.Products.Any())
			{
				var productsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);


				if (products?.Count > 0)
				{
					await _dbContext.Set<Product>().AddRangeAsync(products);
					await _dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
	

