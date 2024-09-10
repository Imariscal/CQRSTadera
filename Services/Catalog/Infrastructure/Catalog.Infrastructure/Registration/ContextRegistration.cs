using Microsoft.Extensions.DependencyInjection;
using Catalog.Infrastructure.Repositories.Base;
using Catalog.Persistence.Contexts.Base;
using Catalog.Persistence.Contexts;
using Catalog.Persistence.Interceptors;

namespace Catalog.Infrastructure.Registration;

public static class ContextRegistration
{
    public static IServiceCollection RegisterContext(this IServiceCollection services)
    {
        services.AddDbContext<ReadOnlyContext>();
        services.AddDbContext<WriteOnlyContext>(o => o.AddInterceptors(new WriteOnlyCommandInterceptor()));
        services.AddTransient<IReadOnlyContext, ReadOnlyContext>();
        services.AddScoped<IWriteOnlyContext, WriteOnlyContext>();
        services.AddScoped<IWriteOnlyUnitOfWork, WriteOnlyUnitOfWork>();
        services.AddScoped<IReadOnlyUnitOfWork, ReadOnlyUnitOfWork>();

        services.AddMemoryCache();
        return services;
    }
}
