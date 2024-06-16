using apbd_tutorial09.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_tutorial09.Data;

public class DoctorConfiguration: IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");
        builder.HasKey(d => d.IdDoctor);
        builder.Property(d => d.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(d => d.LastName).HasMaxLength(100).IsRequired();
        builder.Property(d => d.Email).HasMaxLength(100).IsRequired();
    }
}