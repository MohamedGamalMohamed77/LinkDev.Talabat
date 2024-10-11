using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
	{

		[HttpGet] //GET: /api/product
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(string? sort,int? brandId,int? categoryId)
		{
			var products = await serviceManager.ProductService.GetProductsAsync(sort, brandId,categoryId);
			return Ok(products);
		}
		[HttpGet("{id:int}")] //GET: /api/product/id
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{ 
		var product =await serviceManager.ProductService.GetProductAsync(id);

			if (product is null)
				return NotFound(new {statusCode = 404, message = "not found "});
			return Ok(product);
		
		}
		[HttpGet("brands")]
		public async Task<ActionResult<BrandDto>> GetBrands()
		{
			var brands = await serviceManager.ProductService.GetBrandsAsync();

			return Ok(brands);
		}
		[HttpGet("categories")]
		public async Task<ActionResult<BrandDto>> GetCategories()
		{
			var categories = await serviceManager.ProductService.GetCatigoriesAsync();

			return Ok(categories);
		}

	}
}
