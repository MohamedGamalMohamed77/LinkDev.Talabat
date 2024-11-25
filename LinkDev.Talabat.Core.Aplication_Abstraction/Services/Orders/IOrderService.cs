using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Aplication.Abstraction.Services.Orders
{
	public interface IOrderService
	{
		Task<OrderToRetunDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);
		Task<OrderToRetunDto> GetOrderByIdAsync(string buyerEmail, int orderId);
		Task<IEnumerable<OrderToRetunDto>> GetOrdersForUserAsync(string buyerEmail);
		Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();




	}
}
