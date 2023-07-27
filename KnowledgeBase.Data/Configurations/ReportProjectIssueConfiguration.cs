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
    public class ReportProjectIssueConfiguration : IEntityTypeConfiguration<ReportProjectIssue>
    {
        public void Configure(EntityTypeBuilder<ReportProjectIssue> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IssueType)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(u => u.ReportProjectsIssues)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Project)
                .WithMany(p => p.ReportedIssues)
                .HasForeignKey(x => x.ProjectId);
        }
    }
}
