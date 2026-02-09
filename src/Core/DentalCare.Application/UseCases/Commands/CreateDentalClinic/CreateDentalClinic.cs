using System;
using DentalCare.Application.Contracts.Persistence;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Application.Utils.Mediator;
using DentalCare.Domain.Entities;
using DentalCare.Domain.ValueObjects;

namespace DentalCare.Application.UseCases.Commands.CreateDentalClinic;

public sealed class CreateDentalClinic
{
    public record CommandCreateDentalClinic : IRequest<Guid>
    {
        public required string Name { get; set; }
    }

    public class CommandHandlerCreateDentalClinic(
        IDentalClinicRepository dentalClinicRepository, 
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CommandCreateDentalClinic, Guid>
    {
        private readonly IDentalClinicRepository _dentalClinicRepository = dentalClinicRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Guid> HandleAsync(CommandCreateDentalClinic command)
        {
            var name = new Name(command.Name);
            var dentalClinic = new DentalClinic(name);
            
            try
            {
                var result = await _dentalClinicRepository.Create(dentalClinic);
                await _unitOfWork.Save();
                return result.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }

}
