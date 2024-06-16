using apbd_tutorial09.DTOs;
using apbd_tutorial09.Exceptions;
using apbd_tutorial09.Mappers;
using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial09.Repositories.Implementation;

public class PatientRepository: IPatientRepository
{
    private readonly AppDbContext _dbContext;

    public PatientRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsPatientExistsAsync(int id)
    {
        return await _dbContext.Patients.AnyAsync(p => p.IdPatient == id);
    }

    public async Task AddPatientAsync(Patient patient)
    {
        await _dbContext.Patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PatientResponseDto> GetPatientAsync(int id)
    {
        var patient = await _dbContext.Patients.FindAsync(id);
        if (patient == null)
        {
            throw new PatientNotFound(id);
        }

        return patient.PatientToResponseDto();
    }
}