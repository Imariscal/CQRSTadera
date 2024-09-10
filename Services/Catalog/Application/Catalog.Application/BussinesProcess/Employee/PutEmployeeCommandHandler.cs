using Catalog.Application.BussinesProcess.Base.Contracts;
using Catalog.Application.DTOs.Employee;
using Catalog.CrossCutting.Exceptions;
using Catalog.Domain.BusinessRules.Base;
using Catalog.Domain.Repositories.Base;
using Mapster;
using MapsterMapper;
 
namespace Catalog.Application.BussinesProcess.Employee
{
    public class PutEmployeeCommandHandler(
        IWriteOnlyRepository<Guid, Catalog.Retail.Domain.Entities.Employee> repository,
        IReadOnlyRepository<Guid, Catalog.Retail.Domain.Entities.Employee> readOnlyRepository,
        IValidationStrategy<EmployeeDTO> validationStrategy) : ICommandHandler<PutEmployeeCommand>
    {
        public async Task Handle(PutEmployeeCommand command)
        {
            ArgumentNullException.ThrowIfNull(command.Id);
            ArgumentNullException.ThrowIfNull(command.Employee);
            
            Validate(command.Employee);

            var result = await readOnlyRepository.GetAsync(command.Id) ?? throw new NotFoundException("Employee was not found");
            command.Employee.Adapt(result);

            await repository.Modify(result);
        }

        private void Validate(EmployeeDTO employeeDTO)        
        {     
            var validationResult = validationStrategy.Validate(employeeDTO);
            if (!validationResult.IsValid) throw new BusinessValidationException(validationResult.Errors);
        }
    }
}
