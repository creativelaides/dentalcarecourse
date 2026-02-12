using System;
using DentalCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalCare.Persistence;

public class DentalCareDbContext : DbContext
{
    public DbSet<DentalClinic> DentalClinics { get; set; }
    
    protected DentalCareDbContext()
    {}

    public DentalCareDbContext(DbContextOptions<DentalCareDbContext> options):base (options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DentalCareDbContext).Assembly);
    }
    
}
