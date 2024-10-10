using LinkDev.Talabat.Core.Domain.Entities;
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
		public ProductWithBrandAndCategorySpecifications() : base()
		{
			AddIncludes();
		}
		// this constructor is created for building query that return specific product with it's brand and category
		public ProductWithBrandAndCategorySpecifications(int id) : base(id)
		{
			Includes.Add(P => P.Brand!);
			Includes.Add(P => P.Category!);
		}
		private void AddIncludes()
		{
			Includes.Add(P => P.Brand!);
			Includes.Add(P => P.Category!);
		}


	}
}
