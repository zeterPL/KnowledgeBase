using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.Extensions.Azure;

namespace KnowledgeBase.Logic.Services
{
    public class UserResourcePermissionService : IUserResourcePermissionService
    {
        private readonly IUserResourcePermissionRepository _resourcePermissionRepository;

        public UserResourcePermissionService(IUserResourcePermissionRepository resourcePermissionRepository)
        {
            _resourcePermissionRepository = resourcePermissionRepository;
        }

        private UserResourcePermission GetResourcePermissionFromDto(ResourcePermissionDto permissionDto)
        {
            UserResourcePermission permission = new UserResourcePermission
            {
                Id = permissionDto.Id,
                UserId = permissionDto.UserId,
                ResourceId = permissionDto.ResourceId,
                Name = permissionDto.PermissionName
            };
            return permission;
        }

        public void Add(ResourcePermissionDto permissionDto)
        {
            var permission = GetResourcePermissionFromDto(permissionDto);
            _resourcePermissionRepository.Add(permission);
        }

        public bool CheckIfUserHavePermissionToResource(Guid resourceId, Guid userId, ResourcePermissionName permissionName)
        {
            var result = _resourcePermissionRepository.CheckIfUserHasPermissionsToResource(resourceId, userId, permissionName);
            return result;
        }

        public void Delete(ResourcePermissionDto permissionDto)
        {
            var permission = GetResourcePermissionFromDto(permissionDto);
            _resourcePermissionRepository.Remove(permission);
        }

        public IList<ResourcePermissionDto> GetAll()
        {
            return _resourcePermissionRepository.GetAll()
                .Select(p => p.ToPermissionDto()).ToList();
        }

        public ResourcePermissionDto? GetById(Guid id)
        {
            var permission = _resourcePermissionRepository.Get(id);
            if (permission is null) return null;
            else return permission.ToPermissionDto();
        }
    }
}