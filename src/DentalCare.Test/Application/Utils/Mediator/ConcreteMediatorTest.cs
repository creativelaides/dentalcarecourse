using System;
using System.Threading.Tasks;
using DentalCare.Application.Exceptions;
using DentalCare.Application.Utils.Mediator;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace DentalCare.Test.Application.Utils.Mediator;

public class ConcreteMediatorTest
{
    public class FakeRequest : IRequest<string>
    {
        public required string Name { get; set; }
    }

    public class FakeRequestHandler : IRequestHandler<FakeRequest, string>
    {
        public Task<string> HandleAsync(FakeRequest request)
        {
            return Task.FromResult($"Respuesta Correcta de {nameof(FakeRequest)}");
        }
    }

    public class FakeValidatorRequest : AbstractValidator<FakeRequest>
    {
        public FakeValidatorRequest()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    [Fact]
    public async Task Should_CallMethodHandler_When_FakeRequestIsReceived()
    {
        // Given
        var request = new FakeRequest(){Name = "TestRequest"};
        var expectedResponse = "Respuesta desde el Handler";
        var mockUseCase = Substitute.For<IRequestHandler<FakeRequest, string>>();
        var mockServiceProvider = Substitute.For<IServiceProvider>();

        // Configurar el mock para devolver un valor específico
        mockUseCase.HandleAsync(request).Returns(Task.FromResult(expectedResponse));

        mockServiceProvider
            .GetService(typeof(IRequestHandler<FakeRequest, string>))
            .Returns(mockUseCase);

        var mediator = new ConcreteMediator(mockServiceProvider);

        // When
        var result = await mediator.SendAsync(request);

        // Then
        await mockUseCase.Received(1).HandleAsync(request);
        Assert.Equal(expectedResponse, result);
    }

    [Fact]
    public void Should_ThrowExceptionValidation_When_NotValidCommand_IsNotValid()
    {
        // Given
        var request = new FakeRequest() { Name = "" };
        var mockServiceProvider = Substitute.For<IServiceProvider>();
        var validator = new FakeValidatorRequest();

        mockServiceProvider
            .GetService(typeof(IValidator<FakeRequest>))
            .Returns(validator);

        var mediator = new ConcreteMediator(mockServiceProvider);
        // When

        // Then
        await Assert.ThrowsAsync<ApplicationValidationException>(() => mediator.SendAsync(request));

    }

    [Fact]
    public async Task Should_ThrowMediatorException_When_UnRegisteredHandler()
    {
        // Given
        var request = new FakeRequest() { Name = "TestRequest" };
        var expectedResponse = "Respuesta desde el Handler";
        var mockUseCase = Substitute.For<IRequestHandler<FakeRequest, string>>();
        var mockServiceProvider = Substitute.For<IServiceProvider>();

        // Configurar el mock para devolver un valor específico
        mockUseCase.HandleAsync(request).Returns(Task.FromResult(expectedResponse));

        // UnRegistered Service Mock
        // mockServiceProvider
        //     .GetService(typeof(IRequestHandler<FakeRequest, string>))
        //     .Returns(mockUseCase);

        var mediator = new ConcreteMediator(mockServiceProvider);

        // When & Then
        await Assert.ThrowsAsync<MediatorException>(() => mediator.SendAsync(request));
    }
}