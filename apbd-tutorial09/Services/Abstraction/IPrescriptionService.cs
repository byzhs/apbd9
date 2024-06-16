using apbd_tutorial09.DTOs;
using apbd_tutorial09.Models;

namespace apbd_tutorial09.Services.Abstraction;

public interface IPrescriptionService
{
    Task<Prescription> AddPrescriptionAsync(PrescriptionRequestDto prescriptionRequestDto);
    Task<List<PrescriptionResponseDto>> GetPrescriptionsByPatientId(int id);
}