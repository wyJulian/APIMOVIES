using Domain.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie", schema: "Catalog");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(100);

            builder.Property(m => m.Description)
                .HasMaxLength(500);

            builder.Property(m => m.Category)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(m => m.Category)
                .HasDatabaseName("IX_Movie_Category");
        }
    }
}
