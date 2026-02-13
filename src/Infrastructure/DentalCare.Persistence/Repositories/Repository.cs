using System;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DentalCare.Persistence.Repositories;

public class Repository<T>(DentalCareDbContext context) : IRepository<T> where T : EntityBase
{
    private readonly DentalCareDbContext _context = context;

    public Task<T> Add(T entity)
    {
        _context.Add(entity);
        return Task.FromResult(entity);
    }

    public Task Delete(Guid id)
    {
        _context.Remove(id);
        return Task.CompletedTask;
    }

    public Task Update(T entity)
    {
        _context.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
}
