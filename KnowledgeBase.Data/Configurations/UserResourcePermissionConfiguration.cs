using KnowledgeBase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Configurations
{
    public class UserResourcePermissionConfiguration : IEntityTypeConfiguration<UserResourcePermission>
    {
        public void Configure(EntityTypeBuilder<UserResourcePermission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasConversion<string>();

            builder.HasOne(x => x.User)
                .WithMany(x => x.ResourcePermissions)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Resource)
                .WithMany(x => x.UserPermissions)
                .HasForeignKey(x => x.ResourceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
