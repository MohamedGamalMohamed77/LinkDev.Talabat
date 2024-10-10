using LinkDev.Talabat.Core.Domain.Entities;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specefications.Employees
{
	public class EmployeeWithDepartmentSpecifications : BaseSpecifications<Employee,int>
	{
		public EmployeeWithDepartmentSpecifications() : base()
		{
			Includes.Add(E => E.department!);
		}
		public EmployeeWithDepartmentSpecifications(int id) : base(id)
		{
			Includes.Add(E => E.department!);
		}
	}
}
