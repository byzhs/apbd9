using System.ComponentModel.DataAnnotations;

namespace apbd_tutorial09.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }
}