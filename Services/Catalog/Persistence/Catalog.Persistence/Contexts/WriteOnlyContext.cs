using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Catalog.Persistence.Contexts.Base;
using Catalog.Persistence.Contexts;
using Catalog.Retail.Domain.Entities;

namespace Catalog.Persistence.Contexts;

public class WriteOnlyContext(DbContextOptions<WriteOnlyContext> options) : DbContext(options), IWriteOnlyContext
{
    public bool IsReadOnly => false;
    public bool IsWriteOnly => true;

    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadOnlyContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDatabase");
        optionsBuilder.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        base.OnConfiguring(optionsBuilder);
    }

}
