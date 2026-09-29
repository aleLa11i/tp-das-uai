using Application.Commands;
using Application.DTOs;

namespace Application.Abstractions;

public interface IGetPintorByIdHandler
{
    Task<PintorDTO> HandleAsync(GetPintorByIdQuery obtenerPintoresQuery, CancellationToken ct = default);
}
