using KnowledgeBase.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface IReportProjectIssueService
    {
        public IList<ReportProjectIssueDto> GetAll();
        public ReportProjectIssueDto Get(Guid id);
        public IList<ReportProjectIssueDto> GeByProjectId(Guid projectId);
        public void Create(ReportProjectIssueDto reportProjectIssueDto);
        public void Close(Guid id);
        public void ReOpen(Guid id);
        public void Delete(Guid id);
        public void Update(ReportProjectIssueDto reportDto);
    }
}
