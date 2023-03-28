using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations; 

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder){
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();

        builder.HasOne(o => o.Role)
            .WithMany()
            .HasForeignKey(f => f.RoleId)
            .IsRequired();

        builder.HasMany(m => m.Theses)
            .WithOne(o => o.User)
            .HasForeignKey(f => f.StudentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasMany(m => m.ThesesDefenceUsers)
            .WithOne(o => o.User)
            .HasForeignKey(f => f.UserId)
            .IsRequired();

        builder.HasMany(m => m.Courses)
            .WithOne(o => o.User)
            .HasForeignKey(f => f.MentorId)
            .IsRequired();
    }
}