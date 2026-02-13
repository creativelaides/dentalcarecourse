using System;
using DentalCare.Application.Contracts.Persistence;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Domain.Entities;
using DentalCare.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using static DentalCare.Application.UseCases.Commands.CreateDentalClinic.CreateDentalClinic;

namespace DentalCare.Test.Application.UseCases;

public class CreateDentalClinicTest
{
    private readonly IDentalClinicRepository _dentalClinicRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CommandHandlerCreateDentalClinic _commandHandler;

    public CreateDentalClinicTest()
    {
        _dentalClinicRepository = Substitute.For<IDentalClinicRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _commandHandler = new CommandHandlerCreateDentalClinic(_dentalClinicRepository, _unitOfWork);
    }

    [Fact]
    public async Task Should_Create_DentalClinic_When_Command_IsValid()
    {
        // Given
        var command = new CommandCreateDentalClinic { Name = "Dental Clinic"};

        // When
        var name = new Name("Dental Clinic");
        var dentalClinic = new DentalClinic(name);
        _dentalClinicRepository.Add(Arg.Any<DentalClinic>()).Returns(dentalClinic);
        
        var result = await _commandHandler.HandleAsync(command);

        // Then
        await _dentalClinicRepository.Received(1).Add(Arg.Any<DentalClinic>());
        await _unitOfWork.Received(1).Save();
        Assert.NotEqual(Guid.Empty, result);
    }


    [Fact]
    public async Task Should_RollbackUnitOfWork_When_RepositoryFails()
    {
        // Given
        var command = new CommandCreateDentalClinic { Name = "Dental Clinic" };

        // When
        _dentalClinicRepository.Add(Arg.Any<DentalClinic>()).Throws<Exception>();

        // Then
        await Assert.ThrowsAsync<Exception>(async () => await _commandHandler.HandleAsync(command));
        await _unitOfWork.Received(1).Rollback();
    }
}
