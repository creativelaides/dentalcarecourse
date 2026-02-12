using System;
using DentalCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalCare.Persistence.Configurations;

public class DentalClinicConfiguration : IEntityTypeConfiguration<DentalClinic>
{
    public void Configure(EntityTypeBuilder<DentalClinic> builder)
    {
        builder.ToTable("DentalClinics");
        
        builder.Property(x => x.Name.Value)
        .HasMaxLength(150)
        .IsRequired();
    }
}
