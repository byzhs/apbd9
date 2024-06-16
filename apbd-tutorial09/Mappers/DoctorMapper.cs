using apbd_tutorial09.DTOs;
using apbd_tutorial09.Models;

namespace apbd_tutorial09.Mappers;

public static class DoctorMapper
{
    public static DoctorDto ToDoctorDto(this Doctor doctor)
    {
        return new DoctorDto
        {
            IdDoctor = doctor.IdDoctor,
            FirstName = doctor.FirstName
        };
    }

    public static Doctor ToDoctor(this DoctorDto dto)
    {
        return new Doctor
        {
            IdDoctor = dto.IdDoctor,
            FirstName = dto.FirstName
        };
    }
}