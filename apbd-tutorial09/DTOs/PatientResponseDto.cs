namespace apbd_tutorial09.DTOs;

public class PatientResponseDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public List<PrescriptionResponseDto> Prescriptions { get; set; }
}