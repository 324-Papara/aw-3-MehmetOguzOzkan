using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Para.Data.Configuration;
using Para.Data.Domain;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Para.Data.Context;

public class ParaDbContext : DbContext
{
    public ParaDbContext(DbContextOptions<ParaDbContext> options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerDetailConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerAddressConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerPhoneConfiguration());
        
    }
}