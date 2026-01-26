using DentalCare.Domain.Entities.Base;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Domain.Entities;


public class Patient : EntityBase
{
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;

    public Patient(Name name, Email email)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Email = email;
    }

}