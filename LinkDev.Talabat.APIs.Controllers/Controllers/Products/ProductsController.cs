using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.Core.Aplication.Abstraction.Common;
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
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
		{
			var products = await serviceManager.ProductService.GetProductsAsync(specParams);
			return Ok(products);
		}

		[HttpGet("{id:int}")] //GET: /api/product/id
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{ 
		var product =await serviceManager.ProductService.GetProductAsync(id);

			//if (product is null)
			//	return NotFound(new ApiResponse(404,$"The Product with id : {id} is not found."));
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
