using ContentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentService.Infrastructure.Configurations;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.ToTable("contents");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(c => c.Body)
               .IsRequired();

        builder.Property(c => c.Author)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Status)
               .HasConversion<string>();

        builder.Property(c => c.CreatedAt);
        builder.Property(c => c.UpdatedAt);
    }
}
