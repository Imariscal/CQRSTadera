using Catalog.Application.BusinessProcess.Employee;
using Catalog.Application.BussinesProcess.Base.Contracts;
using Catalog.Application.BussinesProcess.Employee;
using Catalog.Application.DTOs.Employee; 
using Microsoft.Extensions.DependencyInjection;
 
namespace Catalog.Application.Registration
{
    public static class BusinessProcessRegistration
    {
        public static IServiceCollection RegisterBusinessProcess(this IServiceCollection services)
        {
            //  Employees
            services.AddTransient<IQueryHandler<GetEmployeeQuery, IEnumerable<EmployeeDTO>>, GetEmployeeHandler>();

            services.AddTransient<ICommandHandler<PostEmployeeCommand>, PostEmployeeCommandHandler> ();
            services.AddTransient<ICommandHandler<PutEmployeeCommand>, PutEmployeeCommandHandler>();


            return services;
        }
    }
}
