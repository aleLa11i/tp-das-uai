using Application.Commands;
using Application.DTOs;

namespace Application.Abstractions;

public interface IGetPintoresHandler
{
    Task<IReadOnlyList<PintorDTO>> HandleAsync(GetPintoresQuery getPintoresQuery, CancellationToken ct = default);
}
