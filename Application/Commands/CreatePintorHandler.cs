using Application.Abstractions;
using Application.Mappers;
using Domain.Repositories;

namespace Application.Commands;

public sealed record CreatePintorCommand(string Nombre, string Apellido);

public sealed class CreatePintorHandler(IPintorRepository pintorRepository) : ICreatePintorHandler
{

    public async Task<int> HandleAsync(CreatePintorCommand pintorCommand, CancellationToken ct = default)
    {
        var pintor = PintorMapper.ToEntity(pintorCommand);
        return await pintorRepository.CrearPintorAsync(pintor, ct);
    }
}