using Application.Abstractions;
using Domain.Repositories;

namespace Application.Commands;

public sealed record UpdatePintorCommand(int Id, string Nombre, string Apellido);

public class UpdatePintorHandler(IPintorRepository pintorRepository) : IUpdatePintorHandler
{
    public async Task<int> HandleAsync(UpdatePintorCommand updatePintorCommand, CancellationToken ct = default)
    {
        var pintor = await pintorRepository.GetByIdAsync(updatePintorCommand.Id, ct);
        if (pintor == null)
        {
            throw new InvalidOperationException("No existe el pintor");
        }
        
        pintor.Nombre = updatePintorCommand.Nombre;
        pintor.Apellido = updatePintorCommand.Apellido;

        return await pintorRepository.UpdatePintorAsync(pintor, ct);
    }
}
