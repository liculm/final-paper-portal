using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    void IEntityTypeConfiguration<RefreshToken>.Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(k => k.UserId);
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.Token).IsRequired(false);
        builder.Property(p => p.ExpiresDateUtc).IsRequired();
        builder.Property(p => p.CreatedDateUtc).IsRequired();
        builder.Property(p => p.RevokedDateUtc).IsRequired(false);

        builder.HasOne(o => o.User)
            .WithOne(o => o.RefreshToken)
            .HasForeignKey<RefreshToken>(f => f.UserId)
            .IsRequired();
    }
}