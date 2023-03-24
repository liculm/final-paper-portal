using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class ThesisDefenceEntityConfiguration : IEntityTypeConfiguration<ThesisDefence>
{
    void IEntityTypeConfiguration<ThesisDefence>.Configure(EntityTypeBuilder<ThesisDefence> builder){
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();

        builder.HasOne(o => o.Thesis)
            .WithMany()
            .HasForeignKey(f => f.ThesesId)
            .IsRequired();

        builder.HasMany(m => m.ThesesDefenceUsers)
            .WithOne(o => o.ThesisDefence)
            .HasForeignKey(f => f.ThesisDefenceId)
            .IsRequired();
    }
}