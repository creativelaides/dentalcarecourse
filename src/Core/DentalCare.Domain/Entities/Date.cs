using DentalCare.Domain.Entities.Base;
using DentalCare.Domain.Enums;
using DentalCare.Domain.Exceptions;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Domain.Entities;

public class Date : EntityBase
{
    public Guid PatientId { get; private set; }
    public Guid DentistId { get; private set; }
    public Guid ClinicId { get; private set; }
    public DateStatus Status { get; private set; }
    public TimeInterval TimeInterval { get; private set; }

    public Patient? Patient { get; private set; }
    public Dentist? Dentist { get; private set; }
    public DentalClinic? Clinic { get; private set; }


    public Date(
        Guid patientId,
        Guid dentistId,
        Guid clinic,
        TimeInterval timeInterval
    )
    {
        if (timeInterval.StartDate < DateTime.UtcNow)
        {
            throw new DomainException($"La fecha de inicio no puede ser menor a la fecha actual");
        }

        Id = Guid.CreateVersion7();
        PatientId = patientId;
        DentistId = dentistId;
        ClinicId = clinic;
        Status = DateStatus.Scheduled;
        TimeInterval = timeInterval;
    }

    public void Cancel()
    {
        if (Status != DateStatus.Scheduled)
        {
            throw new DomainException("Solo se pueden cancelar citas programadas.");
        }

        Status = DateStatus.Canceled;
    }

    public void Completed()
    {
        if (Status != DateStatus.Scheduled)
        {
            throw new DomainException("Solo se pueden completar citas programadas.");
        }

        Status = DateStatus.Completed;
    }
}
