using apbd_tutorial09.Models;

namespace apbd_tutorial09.DTOs;

public class PatientRequestDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}