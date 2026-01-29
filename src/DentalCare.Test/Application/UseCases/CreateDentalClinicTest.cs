using System;
using DentalCare.Application.Contracts.Persistence;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Application.Exceptions;
using DentalCare.Domain.Entities;
using DentalCare.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using static DentalCare.Application.UseCases.Commands.CreateDentalClinic.CreateDentalClinic;

namespace DentalCare.Test.Application.UseCases;

public class CreateDentalClinicTest
{
    private readonly IDentalClinicRepository _dentalClinicRepository;
    private readonly IValidator<CommandCreateDentalClinic> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CommandHandlerCreateDentalClinic _commandHandler;

    public CreateDentalClinicTest()
    {
        _dentalClinicRepository = Substitute.For<IDentalClinicRepository>();
        _validator = Substitute.For<IValidator<CommandCreateDentalClinic>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _commandHandler = new CommandHandlerCreateDentalClinic(_dentalClinicRepository, _unitOfWork, _validator);
    }

    [Fact]
    public async Task Should_Create_DentalClinic_When_Command_IsValid()
    {
        // Given
        var command = new CommandCreateDentalClinic { Name = "Dental Clinic"};

        // When
        _validator.ValidateAsync(command).Returns(new ValidationResult());

        var name = new Name("Dental Clinic");
        var dentalClinic = new DentalClinic(name);
        _dentalClinicRepository.Create(Arg.Any<DentalClinic>()).Returns(dentalClinic);
        
        var result = await _commandHandler.HandleAsync(command);

        // Then
        await _validator.Received(1).ValidateAsync(command);
        await _dentalClinicRepository.Received(1).Create(Arg.Any<DentalClinic>());
        await _unitOfWork.Received(1).Save();
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task Should_Throw_ApplicationValidationException_When_Command_IsInvalid()
    {
        // Given
        var command = new CommandCreateDentalClinic { Name = ""};

        // When
        var failureResult = new ValidationResult
        (
            new[]
            {
                new ValidationFailure("Name","El nombre es requerido")
            }
        );

        _validator.ValidateAsync(command).Returns(failureResult);

        // Then
        await Assert.ThrowsAsync<ApplicationValidationException>(async () => await _commandHandler.HandleAsync(command));
        await _dentalClinicRepository.DidNotReceive().Create(Arg.Any<DentalClinic>());
    }

    [Fact]
    public async Task Should_RollbackUnitOfWork_When_RepositoryFails()
    {
        // Given
        var command = new CommandCreateDentalClinic { Name = "Dental Clinic" };

        // When
        _dentalClinicRepository.Create(Arg.Any<DentalClinic>()).Throws<Exception>();
        _validator.ValidateAsync(command).Returns(new ValidationResult());

        // Then
        await Assert.ThrowsAsync<Exception>(async () => await _commandHandler.HandleAsync(command));
        await _unitOfWork.Received(1).Rollback();
    }
}
