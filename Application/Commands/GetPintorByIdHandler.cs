using Application.Abstractions;
using Application.DTOs;
using Application.Mappers;
using Domain.Repositories;

namespace Application.Commands;

public record GetPintorByIdQuery(int Id);

public sealed class GetPintorByIdHandler(IPintorRepository pintorRepository) : IGetPintorByIdHandler
{
    public async Task<PintorDTO> HandleAsync(GetPintorByIdQuery obtenerPintoresQuery, CancellationToken ct = default)
    {
        var pintor = await pintorRepository.GetByIdAsync(obtenerPintoresQuery.Id, ct);

        if (pintor == null)
        {
            throw new InvalidOperationException("No se encontró un pintor con ese Id");
        }

        var pintorResponse = PintorMapper.ToDTO(pintor);

        return pintorResponse;
    }
}
