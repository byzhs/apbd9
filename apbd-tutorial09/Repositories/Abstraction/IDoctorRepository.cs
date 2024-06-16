using apbd_tutorial09.Models;

namespace apbd_tutorial09.Repositories.Abstraction;

public interface IDoctorRepository
{
    Task<bool> IsDoctorExistsAsync(int id);
    Task AddDoctorAsync(Doctor doctor);
}