using Domain;
using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    static class ConfigurationExtension
    {
        public static EntityTypeBuilder<T> ConfigureAuditable<T>(this EntityTypeBuilder<T> builder)
            where T : AuditableEntity
        {
            builder.Property(m => m.CreateBy).HasColumnType("int").IsRequired();
            builder.Property(m => m.CreatedAt).HasColumnType("datetime").IsRequired();
            builder.Property(m => m.ModifiedBy).HasColumnType("int").IsRequired(false);
            builder.Property(m => m.ModifiedAt).HasColumnType("datetime").IsRequired(false);
            builder.Property(m => m.DeletedBy).HasColumnType("int").IsRequired(false);
            builder.Property(m => m.DeletedAt).HasColumnType("datetime").IsRequired(false);

            builder.HasOne<OganiUser>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.CreateBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<OganiUser>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.ModifiedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<OganiUser>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.DeletedBy)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }
    }
}
