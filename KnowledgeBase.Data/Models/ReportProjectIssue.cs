using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Data.Models
{
    public class ReportProjectIssue
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsOpen { get; set; }
        public ReportProjectIssuesTypes IssueType { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}