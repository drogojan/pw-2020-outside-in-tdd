using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenChat.Domain.Entities;

namespace OpenChat.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.About)
                .HasMaxLength(100);

            // username should be unique
            builder.HasAlternateKey(p => p.Username);
        }
    }
}