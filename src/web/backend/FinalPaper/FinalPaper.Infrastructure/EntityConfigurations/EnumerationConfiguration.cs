using FinalPaper.Domain.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations; 

public abstract class EnumerationConfiguration<T> : IEntityTypeConfiguration<T> where T : Enumeration {
    public void Configure(EntityTypeBuilder<T> builder) {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
    }
}