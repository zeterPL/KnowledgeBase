using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class ReportProjectIssueRepository : GenericRepository<ReportProjectIssue>, IGenericRepository<ReportProjectIssue>,
        IReportProjectIssueRepository
    {
        public ReportProjectIssueRepository(KnowledgeDbContext context) : base(context) { }                          
    }
}
