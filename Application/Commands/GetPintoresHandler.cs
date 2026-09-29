using Application.Abstractions;
using Application.DTOs;
using Application.Mappers;
using Application.Strategies;
using Domain.Repositories;

namespace Application.Commands;

public record GetPintoresQuery(CriterioOrden Criterio = CriterioOrden.Id);

public sealed class GetPintoresHandler(
    IPintorRepository pintorRepository,
    IEnumerable<IOrdenamientoPintorStrategy> estrategias) : IGetPintoresHandler
{
    public async Task<IReadOnlyList<PintorDTO>> HandleAsync(GetPintoresQuery query, CancellationToken ct = default)
    {
        var estrategia = estrategias.First(e => e.Criterio == query.Criterio);

        var pintores = await pintorRepository.GetPintoresAsync(ct);
        var pintoresDto = pintores.Select(PintorMapper.ToDTO);

        return estrategia.Ordenar(pintoresDto).ToList().AsReadOnly();
    }
}