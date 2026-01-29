using System;
using FluentValidation.Results;

namespace DentalCare.Application.Exceptions;

public class ApplicationValidationException : Exception
{
    public ICollection<string> ApplicationErrors { get; set; } = [];

    public ApplicationValidationException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ApplicationErrors.Add(error.ErrorMessage);
        }
    }

}
