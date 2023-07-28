namespace KnowledgeBase.Data.Models.Interfaces;

public interface IPermissionRequest : IDeletableEntity
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
    public Guid ReceiverId { get; set; }
    public User Receiver { get; set; }
}