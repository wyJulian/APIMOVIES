using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", schema: "Auth");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.NormalizedUserName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
            builder.Property(u => u.NormalizedEmail).HasMaxLength(50).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.NormalizedFirstName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.MiddleName).HasMaxLength(50);
            builder.Property(u => u.NormalizedMiddleName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.NormalizedLastName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.PhoneNumber).HasMaxLength(20);
            builder.Property(u => u.NormalizedPhoneNumber).HasMaxLength(20);
            builder.Property(u => u.TwoFactorChannel).HasMaxLength(10);

            builder.HasIndex(u => u.NormalizedUserName)
                .IsUnique()
                .HasDatabaseName("UQ_Users_NormalizedUserName");

            builder.HasIndex(u => u.NormalizedEmail)
                .IsUnique()
                .HasDatabaseName("UQ_Users_NormalizedEmail");

            builder.HasIndex(u => u.NormalizedPhoneNumber)
                .IsUnique()
                .HasDatabaseName("UQ_Users_NormalizedPhoneNumber");
        }
    }
}
