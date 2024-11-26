using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services
{
	public class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		private readonly Lazy<IOrderService> _orderService;
		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<IEmployeeService> _employeeService;
		private readonly Lazy<IBasketService> _basketService;
		private readonly Lazy<IAuthService> _authService;

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration,Func<IOrderService> orderServiceFactory,Func<IBasketService> basketServiceFactory,Func<IAuthService> authService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_configuration = configuration;
			_productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
			_employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(_unitOfWork, _mapper));

			_orderService = new Lazy<IOrderService>(orderServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
			_basketService = new Lazy<IBasketService>(basketServiceFactory,LazyThreadSafetyMode.ExecutionAndPublication);
			_authService = new Lazy<IAuthService>(authService, LazyThreadSafetyMode.ExecutionAndPublication);
		}

		public IProductService ProductService => _productService.Value;
		public IEmployeeService EmployeeService => _employeeService.Value;
		public IBasketService BasketService => _basketService.Value;

		public IAuthService AuthService =>_authService.Value;

		public IOrderService OrderService => _orderService.Value;
	}
}
