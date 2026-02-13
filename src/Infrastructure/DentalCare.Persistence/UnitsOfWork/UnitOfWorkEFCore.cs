using System;
using DentalCare.Application.Contracts.Persistence;

namespace DentalCare.Persistence.UnitsOfWork;

public class UnitOfWorkEFCore(DentalCareDbContext context) : IUnitOfWork
{
    private readonly DentalCareDbContext _context = context;

    public Task Rollback()
    {
        return Task.CompletedTask;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
