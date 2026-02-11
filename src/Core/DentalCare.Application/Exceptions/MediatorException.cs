using System;

namespace DentalCare.Application.Exceptions;

public class MediatorException(string message) : Exception(message)
{
    
    private MediatorException() : this(string.Empty)
    {
        
    }
}
