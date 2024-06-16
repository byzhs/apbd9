using apbd_tutorial09.DTOs;

namespace apbd_tutorial09.Services.Abstraction;

public interface IDoctorService
{
    Task AddDoctorIfNotExistsAsync(DoctorDto doctorDto);
}