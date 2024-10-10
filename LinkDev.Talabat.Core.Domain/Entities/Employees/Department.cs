using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Employees
{
	public class Department
	{
		public required string Name { get; set; }
		public TimeOnly CreationDate { get; set; }

	}
}
