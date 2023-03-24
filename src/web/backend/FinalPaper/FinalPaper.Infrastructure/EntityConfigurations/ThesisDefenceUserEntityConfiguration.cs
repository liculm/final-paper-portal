using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class ThesisDefenceUserEntityConfiguration : IEntityTypeConfiguration<ThesisDefenceUser>
{
    public void Configure(EntityTypeBuilder<ThesisDefenceUser> builder)
    {
        builder.HasKey(k => new { k.UserId, k.ThesisDefenceId });

        builder.HasOne(o => o.User)
            .WithMany(m => m.ThesesDefenceUsers)
            .HasForeignKey(f => f.UserId)
            .IsRequired();

        builder.HasOne(o => o.ThesisDefence)
            .WithMany(m => m.ThesesDefenceUsers)
            .HasForeignKey(f => f.ThesisDefenceId)
            .IsRequired();
    }
    
}