using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Dto
{
    public class ReportProjectIssueDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsOpen { get; set; }
        public ReportProjectIssuesTypes IssueType { get; set; }

        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
       
    }

    public static class ReportProjectIssueExtensions
    {
        public static ReportProjectIssueDto toReportProjectIssueDto(this ReportProjectIssue reportProjectIssue)
        {
            return new ReportProjectIssueDto
            {
                Id = reportProjectIssue.Id,
                Title = reportProjectIssue.Title,
                Description = reportProjectIssue.Description,
                CreatedDate = reportProjectIssue.CreatedDate,
                IsOpen = reportProjectIssue.IsOpen,
                IssueType = reportProjectIssue.IssueType,
                ProjectId = reportProjectIssue.ProjectId,
                UserId = reportProjectIssue.UserId
            };
        }
    }
}
