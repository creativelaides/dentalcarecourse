using System;
using DentalCare.Domain.Entities.Base;

namespace DentalCare.Application.Contracts.Repositories;

public interface IRepository<T> where T : EntityBase
{
    Task<T?> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task Delete(Guid id);
}
