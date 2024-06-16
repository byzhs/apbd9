using apbd_tutorial09.Models;

namespace apbd_tutorial09.Repositories.Abstraction;

public interface IPrescriptionMedicamentRepository
{
    Task AddPrescriptionMedicamentAsync(PrescriptionMedicament prescriptionMedicament);
}