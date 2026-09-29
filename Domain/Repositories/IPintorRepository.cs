using Domain.Entities;

namespace Domain.Repositories;

public interface IPintorRepository
{
    Task<int> UpdatePintorAsync(Pintor pintor, CancellationToken ct = default);
    Task<int> CrearPintorAsync(Pintor pintor, CancellationToken ct = default);
    Task<int> EliminarPintorAsync(Pintor pintor, CancellationToken ct = default);
    Task<List<Pintor>> GetPintoresAsync(CancellationToken ct = default);
    Task<Pintor?> GetByIdAsync(int id, CancellationToken ct = default);
}
