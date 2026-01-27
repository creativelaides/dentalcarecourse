using DentalCare.Domain.Exceptions;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Should_CreateEmail_When_ValueIsValid()
    {
        var validEmail = "user@example.com";

        var email = new Email(validEmail);

        Assert.Equal(validEmail, email.Value);
    }

    [Fact]
    public void Should_ThrowDomainException_When_ValueIsNull()
    {
        string nullEmail = null!;

        Assert.Throws<DomainException>(() => new Email(nullEmail));
    }

    [Fact]
    public void Should_ThrowDomainException_When_ValueIsEmpty()
    {
        var emptyEmail = "";

        Assert.Throws<DomainException>(() => new Email(emptyEmail));
    }

    [Fact]
    public void Should_ThrowDomainException_When_EmailDoesNotContainAtSymbol()
    {
        var invalidEmail = "userexample.com";

        Assert.Throws<DomainException>(() => new Email(invalidEmail));
    }
}
