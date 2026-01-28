using System;

namespace DentalCare.Application.UseCases.Commands.CreateDentalClinic;

public sealed class CreateDentalClinic
{
    public record CommandCreateDentalClinic
    {
        public required string Name { get; set; }
    }

    public sealed class CommandHandlerCreateDentalClinic
    {
        public async Task<Guid> HandleAsync(CommandCreateDentalClinic command)
        {
            throw new NotImplementedException();
        }
    }

}
