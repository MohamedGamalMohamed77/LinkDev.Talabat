using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products
{
	internal class ProductCategoryConfigurations:BaseEntiyConfiguration<ProductCategory,int>
	{

		public override void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			builder.Property(B => B.Name).IsRequired();
		}

	}
}
