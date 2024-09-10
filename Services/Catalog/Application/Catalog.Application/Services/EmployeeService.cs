using Catalog.Application.BusinessProcess.Employee; 
using Catalog.Application.BussinesProcess.Base.Contracts;
using Catalog.Application.BussinesProcess.Employee;
using Catalog.Application.DTOs.Employee;
using Catalog.Application.Services.Contracts;

namespace Catalog.Application.Services
{
    public class EmployeeService(IMediator mediator) : IEmployeeService
    { 
        public async Task<IEnumerable<EmployeeDTO>> GetEmployeeData()
        {
            var handler = mediator.GetQueryHandler<GetEmployeeQuery, IEnumerable<EmployeeDTO>>();
            return await handler.Handle(new GetEmployeeQuery());
        }

        public async Task PostEmployee(EmployeeDTO employee)
        {
            var handler = mediator.GetCommandHandler<PostEmployeeCommand>();
            await handler.Handle(new PostEmployeeCommand(employee));
        }

        public async Task UpdteEmployee(Guid employeeId, EmployeeDTO employee)
        {
            var handler = mediator.GetCommandHandler<PutEmployeeCommand>();
            await handler.Handle(new PutEmployeeCommand(employeeId, employee));
        }
    }
}
