﻿using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Models.Interfaces;

namespace KnowledgeBase.Data.Models;

public class ProjectPermissionRequest : IPermissionRequest
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public virtual User Sender { get; set; }
    public Guid ReceiverId { get; set; }
    public virtual User Receiver { get; set; }
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }
    public ICollection<ProjectPermissionName> RequestedPermissions { get; }
}