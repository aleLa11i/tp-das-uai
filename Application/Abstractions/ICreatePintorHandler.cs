using Application.Commands;

namespace Application.Abstractions;

public interface ICreatePintorHandler
{
    Task<int> HandleAsync(CreatePintorCommand pintorCommand, CancellationToken ct = default);
}
