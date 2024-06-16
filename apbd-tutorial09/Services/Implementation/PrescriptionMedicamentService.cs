using apbd_tutorial09.DTOs;
using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using apbd_tutorial09.Services.Abstraction;

namespace apbd_tutorial09.Services.Implementation;

public class PrescriptionMedicamentService: IPrescriptionMedicamentService
{
    private readonly IPrescriptionMedicamentRepository _prescriptionMedicamentRepository;

    public PrescriptionMedicamentService(IPrescriptionMedicamentRepository prescriptionMedicamentRepository)
    {
        _prescriptionMedicamentRepository = prescriptionMedicamentRepository;
    }
    
    public async Task AddPrescriptionMedicamentsAsync(int prescriptionId, List<MedicamentRequestDto> medicaments)
    {
        foreach (var medicamentDto in medicaments)
        {
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdPrescription = prescriptionId,
                IdMedicament = medicamentDto.IdMedicament,
                Dose = medicamentDto.Dose,
                Details = medicamentDto.Description
            };
            await _prescriptionMedicamentRepository.AddPrescriptionMedicamentAsync(prescriptionMedicament);
        }
    }
}