using Application.Commands;

namespace Application.Abstractions;

public interface IUpdatePintorHandler
{
    Task<int> HandleAsync(UpdatePintorCommand updatePintorCommand, CancellationToken ct = default);
}
