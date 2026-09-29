using Application.Abstractions;
using Application.DTOs;
using Application.Mappers;
using Domain.Repositories;

namespace Application.Commands;

public record GetPintoresQuery();

public sealed class GetPintoresHandler(IPintorRepository pintorRepository) : IGetPintoresHandler
{
    public async Task<IReadOnlyList<PintorDTO>> HandleAsync(GetPintoresQuery getPintoresQuery, CancellationToken ct = default)
    {
       var pintores = await pintorRepository.GetPintoresAsync(ct);
       var pintoresResponse = pintores.Select(p => PintorMapper.ToDTO(p)).ToList().AsReadOnly();
       return pintoresResponse;
    }
}