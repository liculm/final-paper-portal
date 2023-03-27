using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
{
    void IEntityTypeConfiguration<Course>.Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(f => f.MentorId)
            .IsRequired();

        builder.HasOne(o => o.CourseType)
            .WithMany()
            .HasForeignKey(f => f.CourseTypeId)
            .IsRequired();

        builder.HasMany(m => m.Theses)
            .WithOne(o => o.Course)
            .HasForeignKey(f => f.CourseId)
            .IsRequired();
    }
}