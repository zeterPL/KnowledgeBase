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
    public class ProjectInterestedUserConfiguration : IEntityTypeConfiguration<ProjectInterestedUser>
    {
        public void Configure(EntityTypeBuilder<ProjectInterestedUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .HasConversion<string>();

            builder.HasOne(x => x.User)
                .WithMany(u => u.ProjectInteresteds)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Project)
                .WithMany(p => p.InterestedUsers)
                .HasForeignKey(x => x.ProjectId);
        }
    }
}
