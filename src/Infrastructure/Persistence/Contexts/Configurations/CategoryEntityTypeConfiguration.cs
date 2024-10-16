﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.Configurations
{
    class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("Categories");

            builder.HasData([
                new (){ Id = 1,Name="Beauty", CreateBy=1, CreatedAt=new DateTime(2024,10,06) },
                new (){ Id = 2,Name="Travel", CreateBy=1, CreatedAt=new DateTime(2024,10,06) }
                ]);
        }
    }
}
