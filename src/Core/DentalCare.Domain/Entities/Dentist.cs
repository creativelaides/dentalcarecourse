using DentalCare.Domain.Entities.Base;
using DentalCare.Domain.Exceptions;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Domain.Entities;


public class Dentist : EntityBase
{
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;

    public Dentist(Name name, Email email)
    {
        Id = Guid.CreateVersion7();
        Name = name ?? throw new DomainException("El objeto nombre no puede ser nulo.");
        Email = email ?? throw new DomainException("El objeto email no puede ser nulo.");
    }
}