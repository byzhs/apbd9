using apbd_tutorial09.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_tutorial09.Data;

public class MedicamentConfiguration: IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.ToTable("Medicament");
        builder.HasKey(m => m.IdMedicament);
        builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Description).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Type).IsRequired().HasMaxLength(100);
    }
}