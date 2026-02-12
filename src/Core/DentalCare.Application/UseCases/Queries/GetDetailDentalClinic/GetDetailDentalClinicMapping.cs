using System;
using DentalCare.Domain.Entities;

namespace DentalCare.Application.UseCases.Queries.GetDetailDentalClinic;

public static class GetDetailDentalClinicMapping
{
    public static GetDetailDentalClinicDTO ToDto(this DentalClinic dentalClinic)
    {
        return new GetDetailDentalClinicDTO
        {
            Id = dentalClinic.Id,
            Name = dentalClinic.Name.Value
        };
    }

}
