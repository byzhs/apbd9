using apbd_tutorial09.DTOs;

namespace apbd_tutorial09.Services.Abstraction;

public interface IPatientService
{
    Task AddPatientIfNotExistsAsync(PatientRequestDto patientDto);
    Task<PatientResponseDto> GetPatientAsync(int id);
}