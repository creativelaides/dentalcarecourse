using System;
using DentalCare.Application.Contracts.Persistence;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Persistence.Repositories;
using DentalCare.Persistence.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalCare.Persistence;

public static class DependencyInjectionPersistence
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<DentalCareDbContext>(options => 
        options.UseNpgsql("name=DentalCareConnectionString"));

        services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();
        services.AddScoped<IDentalClinicRepository, DentalClinicRepository>();

        return services;
    }
}
