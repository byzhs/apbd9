using apbd_tutorial09.DTOs;
using apbd_tutorial09.Models;

namespace apbd_tutorial09.Mappers;

public static class PrescriptionMapper
{
    public static PrescriptionResponseDto PrescriptionResponseDto(this Prescription prescription)
    {
        return new PrescriptionResponseDto
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate
        };
    }

    public static Prescription RequestDtoToPrescription(this PrescriptionRequestDto dto)
    {
        return new Prescription
        {
            IdPatient = dto.Patient.IdPatient,
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdDoctor = dto.Doctor.IdDoctor
        };
    }
}