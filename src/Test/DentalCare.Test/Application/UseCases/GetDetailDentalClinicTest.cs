using System;
using System.Threading.Tasks;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Application.Exceptions;
using DentalCare.Domain.Entities;
using DentalCare.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using static DentalCare.Application.UseCases.Queries.GetDetailDentalClinic.GetDetailDentalClinic;

namespace DentalCare.Test.Application.UseCases;

public class GetDetailDentalClinicTest
{
    private readonly IDentalClinicRepository _repository;
    private readonly QueryHandlerGetDetailDentalClinic _queryHandler;

    public GetDetailDentalClinicTest()
    {
        _repository = Substitute.For<IDentalClinicRepository>();
        _queryHandler = new QueryHandlerGetDetailDentalClinic(_repository);
    }

    [Fact]
    public async Task Should_GetById_DentalClinic_When_Command_IsValid()
    {
        // Given
        var testName = "Dental Clinic Name";
        var name = new Name(testName);
        var dentalClinic = new DentalClinic(name);
        var id = dentalClinic.Id;

        var query = new QueryGetDetailDentalClinic {Id = id};
        _repository.GetById(id).Returns(dentalClinic);
    
        // When
        var result = await _queryHandler.HandleAsync(query);
    
        // Then
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(testName, result.Name);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_When_DentalCareNotExist()
    {
        // Given
        var id = Guid.CreateVersion7();
        var query = new QueryGetDetailDentalClinic {Id = id};
        _repository.GetById(id).ReturnsNull();

        // When & Then
        await Assert.ThrowsAsync<NotFoundException>(async () => await _queryHandler.HandleAsync(query));
    }
}
