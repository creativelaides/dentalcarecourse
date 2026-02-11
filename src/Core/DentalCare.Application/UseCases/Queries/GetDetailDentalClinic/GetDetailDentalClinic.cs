using System;
using DentalCare.Application.Contracts.Repositories;
using DentalCare.Application.Exceptions;
using DentalCare.Application.Utils.Mediator;

namespace DentalCare.Application.UseCases.Queries.GetDetailDentalClinic;

public class GetDetailDentalClinic
{
    public record QueryGetDetailDentalClinic :IRequest<GetDetailDentalClinicDTO>
    {
        public required Guid Id { get; set; }
    }

    public class QueryHandlerGetDetailDentalClinic(
        IDentalClinicRepository repository
    ) 
    : IRequestHandler<QueryGetDetailDentalClinic, GetDetailDentalClinicDTO>
    {
        public readonly IDentalClinicRepository _repository = repository;

        public async Task<GetDetailDentalClinicDTO> HandleAsync(QueryGetDetailDentalClinic request)
        {
            var dentalClinic = await _repository.GetById(request.Id);

            if(dentalClinic is null)
            {
                throw new NotFoundException("Clínica no encontrada");
            }

            var dto = new GetDetailDentalClinicDTO
            {
                Id = dentalClinic.Id,
                Name = dentalClinic.Name.Value
            };
            
            return dto;
        }
    }
}
