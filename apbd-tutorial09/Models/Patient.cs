using System.ComponentModel.DataAnnotations;

namespace apbd_tutorial09.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
    
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }
}