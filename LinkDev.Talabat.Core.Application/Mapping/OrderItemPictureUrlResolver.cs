using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	public class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem,OrderItemDto, string>
	{
			public string Resolve(OrderItem source,OrderItemDto destination, string destMember, ResolutionContext context)
			{
				if (!string.IsNullOrEmpty(source.Product.PictureUrl))
					return $"{configuration["Urls:ApiBaseUrl"]}/{source.Product.PictureUrl}";

				return string.Empty;
			}
	}
}
