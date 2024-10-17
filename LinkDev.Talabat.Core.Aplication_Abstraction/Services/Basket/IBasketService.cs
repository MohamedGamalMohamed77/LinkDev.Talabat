using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Aplication.Abstraction.Services.Basket
{
	public interface IBasketService
	{
		Task<CustomerBasketDto> GetCustomerBasketAsync(string id);

		Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasket);
		Task DeleteCustomerBasketAsync(string id);

	}
}
