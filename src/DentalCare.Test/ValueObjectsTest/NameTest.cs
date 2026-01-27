using DentalCare.Domain.Exceptions;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Test.ValueObjectsTest;

public class NameTest
{
    [Fact]
    public void Should_CreateName_When_ValueIsValid()
    {
        var validName = "Jose Velaides";

        var name = new Name(validName);

        Assert.Equal(validName, name.Value);
    }

    
    [Fact]
    public void Should_ThrowDomainException_When_ValueIsNull()
    {
        string nullName = null!;

        Assert.Throws<DomainException>(() => new Name(nullName));
    }
}
