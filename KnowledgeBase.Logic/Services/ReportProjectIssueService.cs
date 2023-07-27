using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services
{
    public class ReportProjectIssueService : IReportProjectIssueService
    {
        private readonly IReportProjectIssueRepository _reportRepository;

        public ReportProjectIssueService(IReportProjectIssueRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        private ReportProjectIssue toReportProjectIssue(ReportProjectIssueDto projectIssueDto)
        {
            return new ReportProjectIssue
            {
                Id = projectIssueDto.Id,
                Title = projectIssueDto.Title,
                Description = projectIssueDto.Description,
                CreatedDate = projectIssueDto.CreatedDate,
                IsOpen = projectIssueDto.IsOpen,
                UserId = projectIssueDto.UserId,
                ProjectId = projectIssueDto.ProjectId,
                IssueType = projectIssueDto.IssueType,
            };
        }

        public void Close(Guid id)
        {
            var report = _reportRepository.Get(id);
            report.IsOpen = false;
            _reportRepository.Update(report);
        }

        public void Delete(Guid id)
        {
            var report = _reportRepository.Get(id);
            _reportRepository.Remove(report);
        }

        public IList<ReportProjectIssueDto> GetAllByProjectId(Guid projectId)
        {
            var result = _reportRepository.GetAll().Where(x => x.ProjectId == projectId);
            if (!result.Any()) return null;
            else return result.Select(x => x.toReportProjectIssueDto()).ToList();
        }

        public ReportProjectIssueDto Get(Guid id)
        {
            var result = _reportRepository.Get(id);
            if (result is null) return null;
            else return result.toReportProjectIssueDto();
        }

        public IList<ReportProjectIssueDto> GetAll()
        {
            var result = _reportRepository.GetAll();
            if (!result.Any()) return null;
            else return result.Select(x => x.toReportProjectIssueDto()).ToList();
        }

        public void ReOpen(Guid id)
        {
            var result = _reportRepository.Get(id);
            result.IsOpen = true;
            _reportRepository.Update(result);
        }

        public void Update(ReportProjectIssueDto reportDto)
        {
            var tmp = toReportProjectIssue(reportDto);
            _reportRepository.Update(tmp);
        }

        public void Create(ReportProjectIssueDto reportProjectIssueDto)
        {
            var tmp = toReportProjectIssue(reportProjectIssueDto);
            _reportRepository.Add(tmp);
        }

        public IList<ReportProjectIssueDto> GetAllOpened()
        {
            return _reportRepository.GetAll().Where(x => x.IsOpen)
                .Select(x => x.toReportProjectIssueDto()).ToList();
        }

        public IList<ReportProjectIssueDto> GetAllClosed()
        {
            return _reportRepository.GetAll().Where(x => !x.IsOpen)
                .Select(x => x.toReportProjectIssueDto()).ToList();
        }

        public IList<ReportProjectIssueDto> GetOpenedByProjectId(Guid projectId)
        {
            return _reportRepository.GetAll().Where(x => x.ProjectId == projectId && x.IsOpen)
                .Select(x=>x.toReportProjectIssueDto()).ToList();  
        }

        public IList<ReportProjectIssueDto> GetClosedByProjectId(Guid projectId)
        {
            return _reportRepository.GetAll().Where(x => x.ProjectId == projectId && !x.IsOpen)
                .Select(x => x.toReportProjectIssueDto()).ToList();
        }
    }
}
