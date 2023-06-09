using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class ThesisEntityConfiguration : IEntityTypeConfiguration<Thesis>
{
    void IEntityTypeConfiguration<Thesis>.Configure(EntityTypeBuilder<Thesis> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
        builder.Property(p => p.StudentId).IsRequired();
        builder.Property(p => p.ThesisStatusTypeId).IsRequired();
        builder.Property(p => p.CourseId).IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(f => f.StudentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(o => o.Course)
            .WithMany(o=>o.Theses)
            .HasForeignKey(f => f.CourseId)
            .IsRequired();

        builder.HasMany(m => m.ThesisDefences)
            .WithOne(o => o.Thesis)
            .HasForeignKey(f => f.ThesesId)
            .IsRequired();
    }
}