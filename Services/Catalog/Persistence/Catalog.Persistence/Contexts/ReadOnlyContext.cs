using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Catalog.Retail.Domain.Entities;
using Catalog.Persistence.Contexts.Base;
using System.Linq.Expressions;

namespace Catalog.Persistence.Contexts;

public class ReadOnlyContext : DbContext, IReadOnlyContext
{
    public bool IsReadOnly => true;
    public bool IsWriteOnly => false;
 
    public DbSet<Employee> Employees { get; set; }

    public ReadOnlyContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadOnlyContext).Assembly);
        SetFilter(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDatabase");
        optionsBuilder.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        base.OnConfiguring(optionsBuilder);
    }

    //***************************************************************************************************
    //  TODO: We need to check how this method affect the performance, it could affect negatively
    //***************************************************************************************************
    private void SetFilter(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var isActiveProperty = entityType.FindProperty("Active");
            if (isActiveProperty != null && isActiveProperty.ClrType == typeof(bool))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "p");
                var filter = Expression.Lambda(Expression.Property(parameter, isActiveProperty.PropertyInfo!), parameter);
                entityType.SetQueryFilter(filter);
            }
        }
    }

    public override int SaveChanges() =>
        throw new InvalidOperationException("This context is a ready only one.");

    public override int SaveChanges(bool acceptAllChangesOnSuccess) =>
        throw new InvalidOperationException("This context is a ready only one.");

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) =>
        throw new InvalidOperationException("This context is a ready only one.");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        throw new InvalidOperationException("This context is a ready only one.");
}
