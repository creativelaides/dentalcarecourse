using System;
using DentalCare.Application.Contracts.Persistence;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Application.Exceptions;
using DentalCare.Application.Utils.Mediator;
using DentalCare.Domain.Entities;
using DentalCare.Domain.ValueObjects;
using FluentValidation;

namespace DentalCare.Application.UseCases.Commands.CreateDentalClinic;

public sealed class CreateDentalClinic
{
    public record CommandCreateDentalClinic : IRequest<Guid>
    {
        public required string Name { get; set; }
    }

    public class CommandHandlerCreateDentalClinic(
        IDentalClinicRepository dentalClinicRepository, 
        IUnitOfWork unitOfWork,
        IValidator<CommandCreateDentalClinic> validator
        )
    {
        private readonly IDentalClinicRepository _dentalClinicRepository = dentalClinicRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<CommandCreateDentalClinic> _validator = validator;


        public async Task<Guid> HandleAsync(CommandCreateDentalClinic command)
        {
            var validation = await _validator.ValidateAsync(command);

            if(!validation.IsValid)
            {
                throw new ApplicationValidationException(validation);
            }

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
