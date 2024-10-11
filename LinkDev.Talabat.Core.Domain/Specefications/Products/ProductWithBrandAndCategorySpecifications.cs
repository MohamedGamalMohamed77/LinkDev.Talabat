using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specefications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		// this constructor is created for building query that return all products with it's brands and categories
		public ProductWithBrandAndCategorySpecifications(string? sort,int? brandId, int? categoryId) : 
			base(
				P =>
				(!brandId.HasValue || P.BrandId==brandId)
				&&
				(!categoryId.HasValue || P.CategoryId==categoryId)
				)
		{
			AddIncludes();
			AddOrderBy(P => P.Name);
			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "nameDesc":
						AddOrderBy(P => P.Name);
					break;

					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;

					case "priceDesc":
						AddOrderByDesc(P => P.Price);
						break;

					default:
						break;

				}
			}
		}
		// this constructor is created for building query that return specific product with it's brand and category
		public ProductWithBrandAndCategorySpecifications(int id) : base(id)
		{
			AddIncludes();
		}
		private protected override void AddIncludes()
		{
			base.AddIncludes();
			Includes.Add(P => P.Brand!);
			Includes.Add(P => P.Category!);
		}


	}
}
