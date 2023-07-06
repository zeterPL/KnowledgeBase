namespace KnowledgeBase.Data.Models.Interfaces
{
    internal interface IDeletableEntity
    {
        public bool IsDeleted { get; set; }
    }
}
