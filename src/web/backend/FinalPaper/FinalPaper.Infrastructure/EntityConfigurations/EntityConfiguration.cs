using FinalPaper.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations; 

public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity {
    public void Configure(EntityTypeBuilder<T> builder) {
        builder.Property(p => p.CreatedByName).IsRequired().HasMaxLength(200);
        builder.Property(p => p.CreatedDateUtc).IsRequired();
        builder.Property(p => p.ModifiedByName).IsRequired().HasMaxLength(200);
        builder.Property(p => p.ModifiedDateUtc).IsRequired();
    }
}