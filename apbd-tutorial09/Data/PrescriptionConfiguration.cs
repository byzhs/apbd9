using apbd_tutorial09.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_tutorial09.Data;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> b)
    {
        b.ToTable("Prescription");
        b.HasKey(p => p.IdPrescription);
        b.HasOne(p => p.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdPatient);
        b.HasOne(p => p.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdDoctor);
    }
}