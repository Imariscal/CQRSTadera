using Catalog.Application.DTOs.Base; 

namespace Catalog.Application.DTOs.Employee;

public record EmployeeDTO : BaseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
