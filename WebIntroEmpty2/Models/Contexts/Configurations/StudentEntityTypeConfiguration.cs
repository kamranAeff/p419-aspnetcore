﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebIntroEmpty2.Models.Entities;

namespace WebIntroEmpty2.Models.Contexts.Configurations
{
    class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(m => m.Surname).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(m => m.GroupId).HasColumnType("int").IsRequired();

            builder.HasKey(m => m.Id);
            builder.ToTable("Student");

            /*
            builder.HasOne<Group>()
                .WithMany()
                .HasPrincipalKey(m=>m.Id)
                .HasForeignKey(m=>m.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
            */
        }
    }
}
