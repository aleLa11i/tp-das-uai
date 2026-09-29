using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class PintorRepository(MainDbContext context) : IPintorRepository
{
    public async Task<List<Pintor>> GetPintoresAsync(CancellationToken ct = default)
    {
        var pintores = await context.Pintores.AsNoTracking().Where(p => !p.IsDeleted).ToListAsync(ct);
        return pintores;
    }

    public async Task<Pintor?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await context.Pintores.Where(p => p.Id == id && !p.IsDeleted).FirstOrDefaultAsync(ct);
    }

    public async Task<int> CrearPintorAsync(Pintor pintor, CancellationToken ct = default)
    {
        await context.Pintores.AddAsync(pintor, ct);
        return await context.SaveChangesAsync(ct);
    }

    public async Task<int> UpdatePintorAsync(Pintor pintor, CancellationToken ct = default)
    {
        context.Pintores.Update(pintor);
        return await context.SaveChangesAsync(ct);
    }

    public async Task<int> EliminarPintorAsync(Pintor pintor, CancellationToken ct = default)
    {
        pintor.IsDeleted = true;
        return await context.SaveChangesAsync(ct);
    }
}
