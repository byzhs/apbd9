using apbd_tutorial09.DTOs;
using apbd_tutorial09.Models;

namespace apbd_tutorial09.Mappers;

public static class PatientMapper
{
    public static Patient RequestDtoToPatient(this PatientRequestDto patientRequestDto)
    {
        return new Patient
        {
            IdPatient = patientRequestDto.IdPatient,
            FirstName = patientRequestDto.FirstName,
            LastName = patientRequestDto.LastName,
            BirthDate = patientRequestDto.BirthDate
        };
    }

    public static PatientResponseDto PatientToResponseDto(this Patient patient)
    {
        return new PatientResponseDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthday = patient.BirthDate
        };
    }
}