using Catalog.Application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.BussinesProcess.Employee
{
    public class PutEmployeeCommand(Guid Id, EmployeeDTO _employee)
    {
        public Guid Id { get; set; } = Id;
        public EmployeeDTO Employee { get; private set; } = _employee;
    }
    
}
