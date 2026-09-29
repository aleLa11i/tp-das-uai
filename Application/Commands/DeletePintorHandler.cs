using Application.Abstractions;
using Domain.Repositories;

namespace Application.Commands;


public sealed record DeletePintorCommand(int Id);

public class DeletePintorHandler(IPintorRepository pintorRepository) : IDeletePintorHandler
{

    public async Task<int> HandleAsync(DeletePintorCommand updatePintorCommand, CancellationToken ct = default)
    {
        var pintor = await pintorRepository.GetByIdAsync(updatePintorCommand.Id, ct);
        if (pintor == null)
        {
            throw new InvalidOperationException("No existe el pintor");
        }

        return await pintorRepository.EliminarPintorAsync(pintor, ct);
    }
}
