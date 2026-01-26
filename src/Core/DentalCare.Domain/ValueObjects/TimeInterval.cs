using DentalCare.Domain.Exceptions;

namespace DentalCare.Domain.ValueObjects;

public record class TimeInterval
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public TimeInterval(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new DomainException($"La fecha de inicio no puede ser mayor a la fecha de fin");
        }

        StartDate = startDate;
        EndDate = endDate;
    }
}
