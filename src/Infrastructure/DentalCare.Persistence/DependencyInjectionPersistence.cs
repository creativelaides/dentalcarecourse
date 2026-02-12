using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalCare.Persistence;

public static class DependencyInjectionPersistence
{
    public static IServiceCollection AddServicePersistence(this IServiceCollection services)
    {
        services.AddDbContext<DentalCareDbContext>(options => 
        options.UseNpgsql("name=DentalCareConnectionString"));

        return services;
    }
}
