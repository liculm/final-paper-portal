using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations; 

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    void IEntityTypeConfiguration<RefreshToken>.Configure(EntityTypeBuilder<RefreshToken> builder){
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(p => p.Expires).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.Token).HasMaxLength(1500).IsRequired();
    }
}