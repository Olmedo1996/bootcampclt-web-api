using Core.Entities;
using Infraestructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Contexts;

//public partial class para el db context
public partial class ApplicationDbContext: DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public ApplicationDbContext()
    {
        
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
