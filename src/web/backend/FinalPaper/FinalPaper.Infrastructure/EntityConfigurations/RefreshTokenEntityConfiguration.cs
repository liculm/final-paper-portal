using FinalPaper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalPaper.Infrastructure.EntityConfigurations;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    void IEntityTypeConfiguration<RefreshToken>.Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(k => k.Token);
        builder.Property(p => p.Token).IsRequired();
        builder.Property(p => p.ExpiresDateUtc).IsRequired();
        builder.Property(p => p.CreatedDateUtc).IsRequired();
        builder.Property(p => p.RevokedDateUtc).IsRequired(false);
        builder.Property(p => p.ReplacedByToken).IsRequired(false);
        builder.Property(p => p.UserId).IsRequired();

        builder.HasOne(o => o.User)
            .WithMany(m => m.RefreshTokens)
            .HasForeignKey(f => f.UserId)
            .IsRequired();
    }
}