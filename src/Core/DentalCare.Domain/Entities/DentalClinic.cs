using DentalCare.Domain.Entities.Base;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Domain.Entities;


public class DentalClinic : EntityBase
{
    public Name Name { get; private set; } = null!;

    public DentalClinic(Name name)
    {
        Name = name;
        Id = Guid.CreateVersion7();
    }
}