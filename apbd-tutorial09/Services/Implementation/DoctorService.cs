using apbd_tutorial09.DTOs;
using apbd_tutorial09.Mappers;
using apbd_tutorial09.Repositories.Abstraction;
using apbd_tutorial09.Services.Abstraction;

namespace apbd_tutorial09.Services.Implementation;

public class DoctorService: IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }
    
    public async Task AddDoctorIfNotExistsAsync(DoctorDto doctorDto)
    {
        var isDoctorExists = await _doctorRepository.IsDoctorExistsAsync(doctorDto.IdDoctor);
        if (!isDoctorExists)
        {
            var doctor = doctorDto.ToDoctor();
            await _doctorRepository.AddDoctorAsync(doctor);
        }
    }
}