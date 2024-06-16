using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial09.Repositories.Abstraction;

public class MedicamentRepository: IMedicamentRepository
{
    private readonly AppDbContext _dbContext;

    public MedicamentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsMedicamentExistsAsync(int id)
    {
        return await _dbContext.Medicaments.AnyAsync(m => m.IdMedicament == id);
    }
}