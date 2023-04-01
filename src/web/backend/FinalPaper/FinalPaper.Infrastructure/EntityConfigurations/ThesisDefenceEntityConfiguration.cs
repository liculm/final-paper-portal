using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class ThesisDefenceEntityConfiguration : IEntityTypeConfiguration<ThesisDefence>
{
    void IEntityTypeConfiguration<ThesisDefence>.Configure(EntityTypeBuilder<ThesisDefence> builder){
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.ThesesId).IsRequired();
        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.DefenceScore).IsRequired();
        builder.Property(p => p.FinalPaperScore).IsRequired();
        builder.Property(p => p.Room).IsRequired();

        builder.HasOne(o => o.Thesis)
            .WithMany()
            .HasForeignKey(f => f.ThesesId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasMany(m => m.ThesesDefenceUsers)
            .WithOne(o => o.ThesisDefence)
            .HasForeignKey(f => f.ThesisDefenceId)
            .IsRequired();
    }
}