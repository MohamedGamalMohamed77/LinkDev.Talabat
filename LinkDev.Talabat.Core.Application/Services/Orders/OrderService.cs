using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Contracts.Products;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specefications.Orders;
using LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
	public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketService basketService , IPaymentService paymentService) : IOrderService
	{
		public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
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


			//5-Get Delivery Method	
			var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(order.DeliveryMethodId);

			//6-create order

			var orderRepo = unitOfWork.GetRepository<Order, int>();

			var orderSpecs = new OrderByPaymentIntentSpecifications(basket.PaymentIntentId!);

			var existingOrder = await orderRepo.GetWithSpecAsync(orderSpecs);

			if (existingOrder is not null)
			{
				orderRepo.Delete(existingOrder);
				await paymentService.CreateOrUpdatePaymentIntent(basket.Id);
			}


			var orderToCreate = new Order()
			{
				BuyerEmail = buyerEmail,
				ShippingAddress = address,
				Subtotal = subTotal,
				Items = orderItems,
				DeliveryMethod =deliveryMethod,
				PaymentIntentId=basket.PaymentIntentId!
			};
			await orderRepo.AddAsync(orderToCreate);

			//7-save to database
			var created = await unitOfWork.CompeleteAsync() > 0;
			if (!created) throw new BadRequestException("an error has been occured during creating order");
			return mapper.Map<OrderToReturnDto>(orderToCreate);

		}
		public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
		{
			var orderSpecs = new OrderSpecifications(buyerEmail);
			var orders = await unitOfWork.GetRepository<Order, int>().GetAllWithSpecAsync(orderSpecs);
			return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
		}
		public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
		{
			var orderSpecs = new OrderSpecifications(buyerEmail,orderId);
			
			var order = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(orderSpecs);

			if (order is null) throw new NotFoundException(nameof(Order), orderId);

			return mapper.Map<OrderToReturnDto>(order);
		}
		public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
		{
			var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
			return mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
		}




	}
}
