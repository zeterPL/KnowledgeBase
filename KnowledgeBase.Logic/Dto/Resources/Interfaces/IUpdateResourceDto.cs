﻿namespace KnowledgeBase.Logic.Dto.Resources.Interfaces;

public interface IUpdateResourceDto : IResourceAction
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
}