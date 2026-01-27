using DentalCare.Domain.Entities.Base;
using DentalCare.Domain.Exceptions;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Domain.Entities;


public class DentalClinic : EntityBase
{
    public Name Name { get; private set; } = null!;

    public DentalClinic(Name name)
    {
        Id = Guid.CreateVersion7();
        Name = name ?? throw new DomainException("El objeto nombre no puede ser nulo.");
    }
}