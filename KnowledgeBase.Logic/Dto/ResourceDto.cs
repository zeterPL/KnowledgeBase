using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Dto
{
    public class ResourceDto
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid ProjectId { get; set; }
		public virtual ResourceCategory Category { get; set; }
		public Guid UserId { get; set; }
		public bool IsDeleted { get; set; }
	}
}
