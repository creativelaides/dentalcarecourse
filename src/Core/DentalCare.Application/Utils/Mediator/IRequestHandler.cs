using System;

namespace DentalCare.Application.Utils.Mediator;

public interface IRequestHandler<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request);
}
