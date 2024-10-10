using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Domain.Contracts.Products;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Specefications.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Employees
{
	internal  class EmployeeService (IUnitOfWork _unitOfWork, IMapper _mapper): IEmployeeService
	{
		
		public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync()
		{
			var spec = new EmployeeWithDepartmentSpecifications();

			var employees = await _unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);

			var employeesToReturn = _mapper.Map<IEnumerable<EmployeeToReturnDto>>(employees);

			return employeesToReturn;
			
		}
		public async Task<EmployeeToReturnDto> GetEmployeeAsync(int id)
		{
			var spec = new EmployeeWithDepartmentSpecifications();

			var employee = await _unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);

			var employeeToReturn = _mapper.Map<EmployeeToReturnDto>(employee);

			return employeeToReturn;
		}
	}
}
