using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class ThesisEntityConfiguration : IEntityTypeConfiguration<Thesis>
{
    void IEntityTypeConfiguration<Thesis>.Configure(EntityTypeBuilder<Thesis> builder){
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(f => f.StudentId)
            .IsRequired();

        builder.HasOne(o => o.Course)
            .WithMany()
            .HasForeignKey(f => f.CourseId)
            .IsRequired();

        builder.HasMany(m => m.ThesisDefences)
            .WithOne(o => o.Thesis)
            .HasForeignKey(f => f.ThesesId)
            .IsRequired();
    }
}