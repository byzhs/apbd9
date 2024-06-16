using apbd_tutorial09.DTOs;
using apbd_tutorial09.Exceptions;
using apbd_tutorial09.Repositories.Abstraction;
using apbd_tutorial09.Services.Abstraction;

namespace apbd_tutorial09.Services.Implementation;

public class MedicamentService: IMedicamentService
{
    private readonly IMedicamentRepository _medicamentRepository;

    public MedicamentService(IMedicamentRepository medicamentRepository)
    {
        _medicamentRepository = medicamentRepository;
    }
    
    public async Task CheckMedicamentsExistAsync(List<MedicamentRequestDto> medicaments)
    {
        foreach (var medicamentDto in medicaments)
        {
            var isMedicamentExists = await _medicamentRepository.IsMedicamentExistsAsync(medicamentDto.IdMedicament);
            if (!isMedicamentExists)
            {
                throw new MedicamentNotFound(medicamentDto.IdMedicament);
            }
        }
    }

}