using System;

namespace DentalCare.Application.UseCases.Queries.GetDetailDentalClinic;

public record GetDetailDentalClinicDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
