using System;

namespace DentalCare.Application.Utils.Mediator;

public interface IMediator
{
    Task<TReponse> SendAsync<TRequest, TReponse>(TRequest request);
}
