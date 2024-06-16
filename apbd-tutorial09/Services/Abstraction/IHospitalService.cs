using apbd_tutorial09.DTOs;

namespace apbd_tutorial09.Services.Abstraction;

public interface IHospitalService
{
    Task AddPrescriptionWithPatient(PrescriptionRequestDto dto);
    Task<PatientResponseDto> GetPatientDetailsAsync(int id);
}