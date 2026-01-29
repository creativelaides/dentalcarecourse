using System;
using FluentValidation;
using static DentalCare.Application.UseCases.Commands.CreateDentalClinic.CreateDentalClinic;

namespace DentalCare.Application.UseCases.Commands.CreateDentalClinic;

public class ValidatorCreateDentalClinic : AbstractValidator<CommandCreateDentalClinic>
{
    public ValidatorCreateDentalClinic()
    {
        RuleFor( x => x.Name).NotEmpty().WithMessage("El nombre de la clínica es requerido - DevProp: {PropertyName}");
    }
}
