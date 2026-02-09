using System;

namespace DentalCare.Application.Utils.Mediator;

public interface IMediator
{
    Task<TReponse> SendAsync<TReponse>(IRequest<TReponse> request);
}
