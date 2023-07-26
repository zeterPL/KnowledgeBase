namespace KnowledgeBase.Data.Models.Interfaces;

public interface IPermissionRequest 
{
	public Guid Id { get; set; }
	public Guid SenderId { get; set; }
	public Guid ReceiverId { get; set; }
}