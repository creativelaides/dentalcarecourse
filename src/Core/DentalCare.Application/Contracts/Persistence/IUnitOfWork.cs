using System;

namespace DentalCare.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    Task Save();
    Task Rollback();
}
