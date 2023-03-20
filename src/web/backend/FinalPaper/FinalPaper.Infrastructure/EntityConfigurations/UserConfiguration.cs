using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations; 

public abstract class UserConfiguration<T> : IEntityTypeConfiguration<T> where T : User {
    public void Configure(EntityTypeBuilder<T> builder) {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
    }
}