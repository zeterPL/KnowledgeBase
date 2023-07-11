using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.ViewModels
{
    public class PermissionViewModel
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}