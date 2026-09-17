using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
    {
        public void Configure(EntityTypeBuilder<VerificationCode> builder)
        {
            builder.ToTable("VerificationCodes", schema: "Auth");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Code).HasMaxLength(100).IsRequired();
            builder.Property(v => v.Purpose).HasMaxLength(30).IsRequired();
            builder.Property(v => v.Channel).HasMaxLength(10).IsRequired();

            builder.HasIndex(v => v.UserId)
                .HasDatabaseName("IX_VerificationCodes_UserId");
        }
    }
}
