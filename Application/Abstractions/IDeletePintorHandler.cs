using Application.Commands;

namespace Application.Abstractions;

public interface IDeletePintorHandler
{
    Task<int> HandleAsync(DeletePintorCommand updatePintorCommand, CancellationToken ct = default);
}
