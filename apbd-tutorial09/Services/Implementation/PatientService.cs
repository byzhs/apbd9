using apbd_tutorial09.DTOs;
using apbd_tutorial09.Mappers;
using apbd_tutorial09.Repositories.Abstraction;
using apbd_tutorial09.Services.Abstraction;

namespace apbd_tutorial09.Services.Implementation;

public class PatientService: IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task AddPatientIfNotExistsAsync(PatientRequestDto patientDto)
    {
        var isPatientExists = await _patientRepository.IsPatientExistsAsync(patientDto.IdPatient);
        if (!isPatientExists)
        {
            var patient = patientDto.RequestDtoToPatient();
            await _patientRepository.AddPatientAsync(patient);
        }
    }
    
    public async Task<PatientResponseDto> GetPatientAsync(int id)
    {
        return await _patientRepository.GetPatientAsync(id);
    }
}