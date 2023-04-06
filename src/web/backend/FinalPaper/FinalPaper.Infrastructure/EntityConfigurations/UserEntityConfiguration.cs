using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.Username).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Password).HasMaxLength(255).IsRequired();
        builder.Property(p => p.IsActive).IsRequired();
        builder.Property(p => p.FirstName).HasMaxLength(255).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(255).IsRequired();
        builder.Property(p => p.RoleId).IsRequired();

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

        builder.HasMany(m => m.RefreshTokens)
            .WithOne(o => o.User)
            .HasForeignKey(f => f.UserId)
            .IsRequired();
    }
}