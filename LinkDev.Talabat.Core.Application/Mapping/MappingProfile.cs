using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<Product, ProductToReturnDto>()
					.ForMember(P => P.Brand, O => O.MapFrom(src => src.Brand!.Name))
					.ForMember(P => P.Category, O => O.MapFrom(src => src.Category!.Name))
					.ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUlResolver>());

			CreateMap<ProductBrand,BrandDto>();
			CreateMap<ProductCategory, CategoryDto>();
			CreateMap<Employee, EmployeeToReturnDto>();

			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
			CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();


		}
	}
}
