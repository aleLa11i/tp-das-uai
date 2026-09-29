using Application.Abstractions;
using Application.DTOs;

namespace Application.Strategies;

public sealed class OrdenPorApellidoStrategy : IOrdenamientoPintorStrategy
{
    public CriterioOrden Criterio => CriterioOrden.Nombre;

    public IEnumerable<PintorDTO> Ordenar(IEnumerable<PintorDTO> pintores)
        => pintores.OrderBy(p => p.Apellido).ThenBy(p => p.Nombre);
}
