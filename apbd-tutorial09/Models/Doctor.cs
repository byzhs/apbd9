using System.ComponentModel.DataAnnotations;

namespace apbd_tutorial09.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Prescription> Prescriptions { get; set; }
    
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }
}