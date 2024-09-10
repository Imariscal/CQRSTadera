using MapsterMapper;  
using Catalog.Domain.Repositories.Base; 
using Catalog.Application.BussinesProcess.Base.Contracts;
using Catalog.Application.DTOs.Employee;
 

namespace Catalog.Application.BusinessProcess.Employee;

public class GetEmployeeHandler(IReadOnlyRepository<Guid, Catalog.Retail.Domain.Entities.Employee> repository, IMapper mapper) 
    : IQueryHandler<GetEmployeeQuery, IEnumerable<EmployeeDTO>>
{
    public async Task<IEnumerable<EmployeeDTO>> Handle(GetEmployeeQuery query)
    {
        var employees = await repository.GetAllAsync();
        var employeesDTO = mapper.Map<IEnumerable<EmployeeDTO>>(employees);   
        return employeesDTO;
    }

 
}
