using apbd_tutorial09.DTOs;
using apbd_tutorial09.Exceptions;
using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial09.Repositories.Implementation;

public class PrescriptionMedicamentRepository: IPrescriptionMedicamentRepository
{
    private readonly AppDbContext _dbContext;

    public PrescriptionMedicamentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddPrescriptionMedicamentAsync(PrescriptionMedicament prescriptionMedicament)
    {
        await _dbContext.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
        await _dbContext.SaveChangesAsync();
    }
}