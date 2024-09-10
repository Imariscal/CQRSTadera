 using Catalog.Application.DTOs.Employee;

namespace Catalog.Application.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployeeData();

        Task PostEmployee(EmployeeDTO employee);

        Task UpdteEmployee(Guid employeeId,  EmployeeDTO employee);
    }
}
