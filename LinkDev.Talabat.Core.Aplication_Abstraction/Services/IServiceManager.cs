using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Aplication.Abstraction.Services
{
	public interface IServiceManager
	{
		public IProductService ProductService { get; }
		public IEmployeeService EmployeeService { get; }
		public IBasketService BasketService { get; }	
	}
}
