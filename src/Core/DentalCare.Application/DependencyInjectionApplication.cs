using System;
using DentalCare.Application.UseCases.Queries.GetDetailDentalClinic;
using DentalCare.Application.Utils.Mediator;
using Microsoft.Extensions.DependencyInjection;
using static DentalCare.Application.UseCases.Commands.CreateDentalClinic.CreateDentalClinic;
using static DentalCare.Application.UseCases.Queries.GetDetailDentalClinic.GetDetailDentalClinic;

namespace DentalCare.Application;

public static class DependencyInjectionApplication
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IMediator, ConcreteMediator>();
        services.AddScoped<IRequestHandler<CommandCreateDentalClinic, Guid>, CommandHandlerCreateDentalClinic>();
        services.AddScoped<IRequestHandler<QueryGetDetailDentalClinic, GetDetailDentalClinicDTO>, QueryHandlerGetDetailDentalClinic>();

        return services;
    }
}
