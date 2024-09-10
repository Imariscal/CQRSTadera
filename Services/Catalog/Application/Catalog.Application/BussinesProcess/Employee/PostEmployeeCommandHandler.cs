using Catalog.Application.BussinesProcess.Base.Contracts;
using Catalog.Application.DTOs.Employee;
using Catalog.CrossCutting.Exceptions;
using Catalog.Domain.BusinessRules.Base;
using Catalog.Domain.Repositories.Base;
 
using MapsterMapper;
 
namespace Catalog.Application.BussinesProcess.Employee
{
    public class PostEmployeeCommandHandler(
        IWriteOnlyRepository<Guid, Catalog.Retail.Domain.Entities.Employee> repository,
        IValidationStrategy<EmployeeDTO> validationStrategy, IMapper mapper) : ICommandHandler<PostEmployeeCommand>
    {
        public async Task Handle(PostEmployeeCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            Validate(command.employee);

            var employee = mapper.Map<Catalog.Retail.Domain.Entities.Employee>(command.employee);
            await repository.AddAsync(employee);
        }

        private void Validate(EmployeeDTO employeeDTO)        
        {     
            var validationResult = validationStrategy.Validate(employeeDTO);
            if (!validationResult.IsValid) throw new BusinessValidationException(validationResult.Errors);
        }
    }
}
