using Application.Abstractions;
using Application.DTOs;

namespace Application.Strategies;

public sealed class OrdenPorIdStrategy : IOrdenamientoPintorStrategy
{
    public CriterioOrden Criterio => CriterioOrden.Nombre;

    public IEnumerable<PintorDTO> Ordenar(IEnumerable<PintorDTO> pintores)
        => pintores.OrderBy(p => p.Id).ThenBy(p => p.Nombre).ThenBy(p => p.Apellido);
}
