using DentalCare.Domain.Entities;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Test.EntitiesTest;

public class DentalClinicTest
{
    [Fact]
    public void Should_CreateDentalClinic_When_ValuesAreValids()
    {
        // Given
        var name = new Name("Dental Care Clinic");

        // When
        var clinic = new DentalClinic(name);

        // Then
        Assert.NotNull(clinic);
        Assert.Equal(name.Value, clinic.Name.Value);
    }
}
