using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext dbContext)
		{
			if (!dbContext.Brands.Any())
			{

				var currentDirectory = Directory.GetCurrentDirectory();
				var brandData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

				if (brands?.Count > 0)
				{
					await dbContext.Brands.AddRangeAsync(brands);
					await dbContext.SaveChangesAsync();
				}
			}
			if (!dbContext.Categories.Any())
			{
				var categoryData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductBrand>>(categoryData);

				if (categories?.Count > 0)
				{
					await dbContext.Brands.AddRangeAsync(categories);
					await dbContext.SaveChangesAsync();
				}
			}
			if (!dbContext.Products.Any())
			{
				var productData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productData);

				if (products?.Count > 0)
				{
					await dbContext.Products.AddRangeAsync(products);
					await dbContext.SaveChangesAsync();
				}
			}
		}

	}
}
