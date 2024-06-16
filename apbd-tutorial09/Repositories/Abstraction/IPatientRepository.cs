using apbd_tutorial09.DTOs;
using apbd_tutorial09.Models;

namespace apbd_tutorial09.Repositories.Abstraction;

public interface IPatientRepository
{
    Task<bool> IsPatientExistsAsync(int id);
    Task AddPatientAsync(Patient patient);
    Task<PatientResponseDto> GetPatientAsync(int id);
}