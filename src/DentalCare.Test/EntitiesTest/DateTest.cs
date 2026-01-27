using DentalCare.Domain.Entities;
using DentalCare.Domain.Enums;
using DentalCare.Domain.Exceptions;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Test.EntitiesTest;

public class DateTest
{
    // Global Given
    private readonly Guid patientId = Guid.CreateVersion7();
    private readonly Guid dentistId = Guid.CreateVersion7();
    private readonly Guid clinicId = Guid.CreateVersion7();
    private readonly TimeInterval timeInterval = new(
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(15)
        );

    [Fact]
    public void Should_CreateDate_When_DataIsValid()
    {
        // When
        var date = new Date(patientId, dentistId, clinicId, timeInterval);

        // Then
        Assert.NotNull(date);
        Assert.Equal(patientId, date.PatientId);
        Assert.Equal(dentistId, date.DentistId);
        Assert.Equal(clinicId, date.ClinicId);
        Assert.Equal(timeInterval, date.TimeInterval);
        Assert.Equal(DateStatus.Scheduled, date.Status);
        Assert.NotEqual(Guid.Empty, date.Id);
    }


    [Fact]
    public void Should_ThrowDomainException_When_StartDateIsLaterThanCurrentDate()
    {
        // Given
        TimeInterval badTimeInterval = new(
            DateTime.UtcNow.AddDays(-15),
            DateTime.UtcNow.AddDays(15)
        );

        // When_Then
        Assert.Throws<DomainException>(() => new Date(patientId, dentistId, clinicId, badTimeInterval));
    }

    [Fact]
    public void Should_CanceledDate_When_DateStateIsScheduled()
    {
        // When
        var date = new Date(patientId, dentistId, clinicId, timeInterval);
        date.Cancel();

        // Then
        Assert.NotNull(date);
        Assert.Equal(DateStatus.Canceled, date.Status);
    }

    [Fact]
    public void Should_ThrowDomainException_When_Canceling_If_StateIsNotScheduled()
    {
        // When
        var date = new Date(patientId, dentistId, clinicId, timeInterval);
        date.Cancel();

        // Then
        Assert.Throws<DomainException>(() => date.Cancel());
    }

    // TODO: Should_CompletedDate_When_DateStateIsScheduled()
    // TODO: Should_ThrowDomainException_When_Canceling_If_StateIsNotScheduled()
}
