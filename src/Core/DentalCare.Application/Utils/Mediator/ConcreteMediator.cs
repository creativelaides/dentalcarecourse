using System;
using DentalCare.Application.Exceptions;
using FluentValidation;

namespace DentalCare.Application.Utils.Mediator;

public class ConcreteMediator(IServiceProvider serviceProvider) : IMediator
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<TReponse> SendAsync<TReponse>(IRequest<TReponse> request)
    {
        var typeOfValidator = typeof(IValidator<>).MakeGenericType(request.GetType());

        var validator = _serviceProvider.GetService(typeOfValidator);

        if (validator is not null)
        {
            var validateMethod = typeOfValidator.GetMethod("ValidateAsync");
            var validateTask = (Task)validateMethod!.Invoke(validator, [request, CancellationToken.None])!;
            await validateTask.ConfigureAwait(false);

            var result = validateTask.GetType().GetProperty("Result");
            var validationResult = (FluentValidation.Results.ValidationResult)result!.GetValue(validateTask)!;

            if (!validationResult.IsValid)
            {
                throw new ApplicationValidationException(validationResult);
            };
        }

        var typeOfUseCase = typeof(IRequestHandler<,>)
        .MakeGenericType(request.GetType(), typeof(TReponse));

        var useCase = _serviceProvider.GetService(typeOfUseCase)
        ?? throw new MediatorException($"No se encontro el handler para la request {request.GetType().Name}");

        var method = typeOfUseCase.GetMethod("HandleAsync")!;

        return await (Task<TReponse>)method.Invoke(useCase, [request])!;
    }
}
