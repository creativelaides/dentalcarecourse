using DentalCare.Domain.Exceptions;

namespace DentalCare.Domain.ValueObjects;

public record class Email
{
    public string Value { get;} = null!;

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException($"El {nameof(value)} es obligatorio.");
        }

        if (!value.Contains('@'))
        {
            throw new DomainException($"El {nameof(value)} no es valido.");
        }

        Value = value;
    }
}
