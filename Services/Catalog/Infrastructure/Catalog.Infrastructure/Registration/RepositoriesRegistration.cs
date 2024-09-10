using Microsoft.Extensions.DependencyInjection; 
using Catalog.Domain.Repositories.Base; 
using Catalog.Infrastructure.Repositories.Base;

namespace Catalog.Infrastructure.Registration;

public static class RepositoriesRegistration
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
        services.AddScoped(typeof(IWriteOnlyRepository<,>), typeof(WriteOnlyRepository<,>));

        return services;
    }
}
