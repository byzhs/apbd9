using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial09.Repositories.Implementation;

public class DoctorRepository: IDoctorRepository
{
    private readonly AppDbContext _dbContext;

    public DoctorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsDoctorExistsAsync(int id)
    {
        return await _dbContext.Doctors.AnyAsync(d => d.IdDoctor == id);
    }
    
    public async Task AddDoctorAsync(Doctor doctor)
    {
        await _dbContext.Doctors.AddAsync(doctor);
        await _dbContext.SaveChangesAsync();
    }
}