using FinalPaper.Domain.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations; 

public abstract class EnumConfiguration<T> : IEntityTypeConfiguration<T> where T : Enumeration {
    public void Configure(EntityTypeBuilder<T> builder) {
        builder.HasKey(d => d.Id);
        builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
    }
}