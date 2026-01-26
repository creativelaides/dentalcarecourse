using DentalCare.Domain.Exceptions;

namespace DentalCare.Domain.ValueObjects;

public record class Name
{
    public string Value { get; } = null!;

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException($"El {nameof(value)} es obligatorio.");
        }

        Value = value;
    }
}
