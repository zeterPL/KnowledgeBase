﻿using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Dto.Project;

public class RequestPermissionDto
{
    public Guid ProjectId { get; set; }
    public Guid SenderId { get; set; }
    public IEnumerable<ProjectPermissionName> AvailablePermissions { get; init; } = Enum.GetValues<ProjectPermissionName>();
    public IEnumerable<ProjectPermissionName>? Permissions { get; set; }
}