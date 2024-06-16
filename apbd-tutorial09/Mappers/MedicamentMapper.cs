using apbd_tutorial09.DTOs;
using apbd_tutorial09.Models;

namespace apbd_tutorial09.Mappers;

public static class MedicamentMapper
{
    public static MedicamentResponseDto MedicamentResponseDto(this Medicament medicament)
    {
        return new MedicamentResponseDto
        {
            IdMedicament = medicament.IdMedicament,
            Name = medicament.Name,
            Description = medicament.Description
        };
    }
}