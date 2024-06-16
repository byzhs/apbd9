using apbd_tutorial09.Models;

namespace apbd_tutorial09.DTOs;

public class PrescriptionRequestDto
{
    public PatientRequestDto Patient { get; set; }
    public List<MedicamentRequestDto> Medicaments { get; set; }
    public DoctorDto Doctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
}