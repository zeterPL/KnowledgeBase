namespace KnowledgeBase.Shared;

public static class GuidExtensions
{
    public static Guid ToGuid(this Guid? source)
    {
        return source ?? Guid.Empty;
    }
}