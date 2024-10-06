using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class BlogPostEntityTypeConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Title).HasColumnType("nvarchar").HasMaxLength(300).IsRequired();
            builder.Property(m => m.Slug).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            builder.Property(m => m.ImagePath).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(m => m.Body).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(m => m.CategoryId).HasColumnType("int").IsRequired();
            builder.Property(m => m.PublisherId).HasColumnType("int").IsRequired(false);
            builder.Property(m => m.PublishDate).HasColumnType("datetime").IsRequired(false);

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("BlogPosts");

            builder.HasOne<Category>()
                .WithMany()
                .HasPrincipalKey(m=>m.Id)
                .HasForeignKey(m=>m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
