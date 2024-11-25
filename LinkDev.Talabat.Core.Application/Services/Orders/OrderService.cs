using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
	public class OrderService(UnitOfWork unitOfWork,IMapper mapper,IBasketService basketService) : IOrderService
	{
		public async Task<OrderToRetunDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
		{
			//1-get basket fom basket repo
			var basket = await basketService.GetCustomerBasketAsync(order.BasketId);

			//2- get selected items basket from products repo

			var orderItems = new List<OrderItem>();
			if (basket.Items.Count() > 0)
			{
				var productRepo = unitOfWork.GetRepository<Product, int>();

				foreach (var item in basket.Items)
				{
					var product = await productRepo.GetAsync(item.Id);
					if (product != null)
					{
						var productItemOrdered = new ProductItemOrdered()
						{
							ProductId = product.Id,
							ProductName = product.Name,
							PictureUrl = product.PictureUrl ?? ""
						};

						var orderItem = new OrderItem()
						{
							Product = productItemOrdered,
							Price = item.Price,
							Quantity = item.Quantity,
						};
						orderItems.Add(orderItem);
					}
				}

			}

			//3-calculate subtotal

			var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

			//4-map address
			var address = mapper.Map<Address>(order.ShippingAddress);

			//5-create order
			var orderToCreate = new Order()
			{
				BuyerEmail = buyerEmail,
				ShippingAddress = address,
				Subtotal = subTotal,
				Items = orderItems,
				DeliveryhMethodId = order.DeliveryMethodId,
			};
			await unitOfWork.GetRepository<Order, int>().AddAsync(orderToCreate);

			//6-save to database
			var created = await unitOfWork.CompeleteAsync() > 0;
			if (!created) throw new BadRequestException("an error has been occured during creating order");
			return mapper.Map<OrderToRetunDto>(orderToCreate);

		}

		public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
		{
			throw new NotImplementedException();
		}

		public Task<OrderToRetunDto> GetOrderByIdAsync(string buyerEmail, int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<OrderToRetunDto>> GetOrdersForUserAsync(string buyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}
