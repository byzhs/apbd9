using apbd_tutorial09.DTOs;
using apbd_tutorial09.Models;

namespace apbd_tutorial09.Repositories.Abstraction;

public interface IPrescriptionRepository
{
    Task<Prescription> AddPrescriptionAsync(Prescription prescription);
    Task<List<PrescriptionResponseDto>> GetPrescriptionsByPatientIdAsync(int id);
}