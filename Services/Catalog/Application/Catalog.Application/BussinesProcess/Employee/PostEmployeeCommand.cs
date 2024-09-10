using Catalog.Application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.BussinesProcess.Employee
{
    public class PostEmployeeCommand
    {
        public EmployeeDTO employee { get; set; }
        public PostEmployeeCommand(EmployeeDTO _employee)
        {
            employee = _employee;
        }
    }
}
