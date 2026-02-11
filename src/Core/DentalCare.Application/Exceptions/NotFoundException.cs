using System;

namespace DentalCare.Application.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
    
}
