using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Common;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;

using UserAddress = LinkDev.Talabat.Core.Domain.Entities.Identity.Address;
using OrderAddress = LinkDev.Talabat.Core.Domain.Entities.Orders.Address;
using LinkDev.Talabat.Shared.Models.Basket;
namespace LinkDev.Talabat.Core.Application.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductToReturnDto>()
					.ForMember(P => P.Brand, O => O.MapFrom(src => src.Brand!.Name))
					.ForMember(P => P.Category, O => O.MapFrom(src => src.Category!.Name))
					.ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

			CreateMap<ProductBrand, BrandDto>();
			CreateMap<ProductCategory, CategoryDto>();
			CreateMap<Employee, EmployeeToReturnDto>();

			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
			CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();

			CreateMap<Order, OrderToReturnDto>()
				.ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod!.ShortName));

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
				.ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
				.ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>());

			CreateMap<OrderAddress, AddressDto>().ReverseMap();

			CreateMap<DeliveryMethod, DeliveryMethodDto>();

			CreateMap<UserAddress, AddressDto>().ReverseMap();
		}
	}
}
