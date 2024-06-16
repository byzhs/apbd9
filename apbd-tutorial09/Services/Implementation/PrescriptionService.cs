using apbd_tutorial09.DTOs;
using apbd_tutorial09.Mappers;
using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using apbd_tutorial09.Services.Abstraction;

namespace apbd_tutorial09.Services.Implementation;

public class PrescriptionService: IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<Prescription> AddPrescriptionAsync(PrescriptionRequestDto prescriptionRequestDto)
    {
        return await _prescriptionRepository.AddPrescriptionAsync(prescriptionRequestDto.RequestDtoToPrescription());
    }

    public async Task<List<PrescriptionResponseDto>> GetPrescriptionsByPatientId(int id)
    {
        return await _prescriptionRepository.GetPrescriptionsByPatientIdAsync(id);
    }
}