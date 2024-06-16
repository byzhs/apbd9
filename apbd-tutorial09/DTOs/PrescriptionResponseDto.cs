namespace apbd_tutorial09.DTOs;

public class PrescriptionResponseDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentResponseDto> Medicaments { get; set; }
    public DoctorDto Doctor { get; set; }
}