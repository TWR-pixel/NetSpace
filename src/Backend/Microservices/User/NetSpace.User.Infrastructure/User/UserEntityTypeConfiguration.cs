using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Infrastructure.User;

public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(b => b.Nickname)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.HasIndex(b => b.Nickname)
            .IsUnique(true);

        builder.Property(b => b.Name)
            .IsRequired(true)
            .HasMaxLength(100);

        builder.Property(b => b.Surname)
            .IsRequired(true)
            .HasMaxLength(100);

        builder.Property(b => b.Email)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.HasIndex(b => b.Email)
            .IsUnique(true);

        builder.Property(b => b.LastName)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(b => b.About)
            .IsRequired(false)
            .HasMaxLength(512);

        builder.Property(b => b.AvatarUrl)
            .IsRequired(false);

        builder.Property(b => b.BirthDate)
            .IsRequired(false);

        builder.Property(b => b.Hometown)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(b => b.CurrentCity)
            .IsRequired(false)
            .HasMaxLength(55);

        builder.Property(b => b.PersonalSite)
            .IsRequired(false);

        builder.Property(b => b.SchoolName)
            .IsRequired(false)
            .HasMaxLength(50);
    }
}
