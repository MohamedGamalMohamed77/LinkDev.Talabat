using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Products;
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
		private readonly Lazy<IProductService>_productService;
		private readonly Lazy<IEmployeeService> _employeeService;
		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork; 
			_mapper = mapper;
			_productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
			_employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(_unitOfWork, _mapper));
		}

		public IProductService ProductService => _productService.Value;
	}
}
