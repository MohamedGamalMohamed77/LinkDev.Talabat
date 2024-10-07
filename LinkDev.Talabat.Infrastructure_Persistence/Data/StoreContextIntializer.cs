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
	public class StoreContextIntializer(StoreContext dbcontext) : IStoreContextIntializer
	{
		public async Task IntializeAsync()
		{
			var pindingMigrations = await dbcontext.Database.GetPendingMigrationsAsync();

			if (pindingMigrations.Any())
				await dbcontext.Database.MigrateAsync();
		}

		public async Task SeedAsync()
		{
			if (!dbcontext.Brands.Any())
			{

				var currentDirectory = Directory.GetCurrentDirectory();
				var brandData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

				if (brands?.Count > 0)
				{
					await dbcontext.Brands.AddRangeAsync(brands);
					await dbcontext.SaveChangesAsync();
				}
			}
			if (!dbcontext.Categories.Any())
			{
				var categoryData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductBrand>>(categoryData);

				if (categories?.Count > 0)
				{
					await dbcontext.Brands.AddRangeAsync(categories);
					await dbcontext.SaveChangesAsync();
				}
			}
			if (!dbcontext.Products.Any())
			{
				var productData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productData);

				if (products?.Count > 0)
				{
					await dbcontext.Products.AddRangeAsync(products);
					await dbcontext.SaveChangesAsync();
				}
			}
		}
	}
}
