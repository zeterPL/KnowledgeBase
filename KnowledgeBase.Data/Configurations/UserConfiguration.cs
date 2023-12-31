﻿using KnowledgeBase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBase.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(u => u.Id);

		builder.Property(u => u.FirstName)
			.HasMaxLength(50);

        builder.Property(u => u.LastName)
            .HasMaxLength(50);
        
        builder.HasMany(u => u.Resources)
            .WithOne(r => r.User);

        builder.HasOne(x => x.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(x => x.RoleId);

        builder.HasMany(x => x.ProjectInteresteds)
            .WithOne(p => p.User)
            .HasForeignKey(x=>x.UserId);
    }
}