using Application.DTOs;
using Application.Strategies;

namespace Application.Abstractions;

public interface IOrdenamientoPintorStrategy
{
    CriterioOrden Criterio { get; }
    IEnumerable<PintorDTO> Ordenar(IEnumerable<PintorDTO> pintores);
}
