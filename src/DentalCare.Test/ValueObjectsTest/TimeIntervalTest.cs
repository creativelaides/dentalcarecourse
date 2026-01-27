using System;
using System.Globalization;
using DentalCare.Domain.Exceptions;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Test;

public class TimeIntervalTest
{
    [Fact]
    public void Should_CreateTimeInterval_When_ValueIsValid()
    {
        // Arrange
        var startDate = new DateTime(2026, 1, 15);
        var endDate = new DateTime(2026, 1, 30);

        // Act
        var timeInterval = new TimeInterval(startDate, endDate);

        // Assert
        Assert.NotNull(timeInterval);
        Assert.Equal(startDate, timeInterval.StartDate);
        Assert.Equal(endDate, timeInterval.EndDate);
    }

    [Fact]
    public void Should_ThrowDomainException_When_StartDateIsAfterEndDate()
    {
        // Arrange
        var startDate = new DateTime(2026, 1, 30);
        var invalidEndDate = new DateTime(2026, 1, 15);

        // Act & Assert
        Assert.Throws<DomainException>(() =>
            new TimeInterval(startDate, invalidEndDate));
    }
}