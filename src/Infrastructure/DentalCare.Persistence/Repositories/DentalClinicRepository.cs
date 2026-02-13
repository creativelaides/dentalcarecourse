using System;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Domain.Entities;

namespace DentalCare.Persistence.Repositories;

public class DentalClinicRepository(DentalCareDbContext context)
: Repository<DentalClinic>(context),
IDentalClinicRepository
{
    
}
