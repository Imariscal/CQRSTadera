
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Infrastructure.Registration; 
using Catalog.Domain.BusinessRules.Base; 
using Catalog.Application.BussinesProcess.Base.Contracts;
using OxxoGas.Catalogos.Retail.Application.BusinessProcess.Base;
using OxxoGas.Catalogos.Retail.Domain.BusinessRules.Base;
using OxxoGas.Catalogos.Retail.Application.Services.Base;
using Catalog.Application.Services.Contracts;
using Catalog.Application.Services;
using FluentValidation;
using Catalog.Application.DTOs.Employee;
using Catalog.Application.BussinesRules.Employee;

namespace Catalog.Application.Registration;

public static class ServiceAppRegister
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        //  Mapper
        services.AddMapster();

        //  Register contexts and repositories
        services.RegisterContext();
        services.RegisterRepositories();

        // Business Process
        services.RegisterBusinessProcess();

        // GRPC
        services.AddScoped<IMediator, Mediator>();
        services.AddTransient(typeof(IValidationStrategy<>), typeof(FluentValidationStrategy<>));

        //  Validadores
        services.AddTransient<IValidator<EmployeeDTO>, AddEmployeeValidation>();

        //  Application services registration
        services.AddTransient(typeof(IBaseCrudApplicationService<,>), typeof(BaseCrudApplicationService<,>)); 
        services.AddTransient<IEmployeeService, EmployeeService>();

        return services;
    }
}
