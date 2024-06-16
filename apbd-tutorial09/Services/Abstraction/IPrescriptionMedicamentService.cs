using apbd_tutorial09.DTOs;

namespace apbd_tutorial09.Services.Abstraction;

public interface IPrescriptionMedicamentService
{
    Task AddPrescriptionMedicamentsAsync(int prescriptionId, List<MedicamentRequestDto> medicaments);
}