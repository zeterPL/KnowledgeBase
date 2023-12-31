﻿namespace KnowledgeBase.Logic.Dto.Resources.Interfaces;

public interface IUpdateResourceDto : IResourceActionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
}