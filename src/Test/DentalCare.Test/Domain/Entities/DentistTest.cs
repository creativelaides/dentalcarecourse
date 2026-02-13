using System;
using DentalCare.Domain.Entities;
using DentalCare.Domain.Exceptions;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Test.Domain.Entities;

public class DentistTest
{
    [Fact]
    public void Should_CreateDentist_When_NameAndEmailAreValids()
    {
        // Given
        var name = new Name("John Doe");
        var email = new Email("mail@example.com");
    
        // When
        var dentist = new Dentist(name, email);
    
        // Then
        Assert.NotNull(dentist);
        Assert.Equal(name.Value, dentist.Name.Value);
        Assert.Equal(email.Value, dentist.Email.Value);
    }

    [Fact]
    public void Should_ThrowDomainException_When_EmailOrNameIsNull()
    {
        // Given
        Name name = null!;
        Email email = null!;
    
        // When_Then
        Assert.Throws<DomainException>(() => new Dentist(name, email));
    }

}
