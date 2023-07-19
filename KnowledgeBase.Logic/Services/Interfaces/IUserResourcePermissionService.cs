using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface IUserResourcePermissionService
    {
        public void Add(ResourcePermissionDto permissionDto);
        public void Delete(ResourcePermissionDto permissionDto);
        public ResourcePermissionDto? GetById(Guid id);
        public IList<ResourcePermissionDto> GetAll();
        public bool CheckIfUserHavePermissionToResource(Guid resourceId, Guid userId, ResourcePermissionName permissionName);
    }
}
