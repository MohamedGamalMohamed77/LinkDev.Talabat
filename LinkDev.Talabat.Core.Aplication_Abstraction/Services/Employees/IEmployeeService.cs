using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Aplication.Abstraction.Services.Employees
{
	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync();
		Task<EmployeeToReturnDto> GetEmployeeAsync(int id);

	}
}
