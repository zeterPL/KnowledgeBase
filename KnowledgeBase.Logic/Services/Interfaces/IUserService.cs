using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Dto.Project;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetAllUsers();

        public UserDto GetById(Guid id);

        public Guid AddUser(UserDto user);

        public UserDto Update(UserDto user);

        public bool Delete(UserDto user);

        public bool SoftDelete(UserDto user);

        public IList<PermissionDto> GetAllUserPermissions(Guid id);

        public void AssignPermissionBasedOnUserRole(RoleDto role, Guid userId);

        public void AddPermisionsToSpecificProject(Guid projectId, Guid userId);

        public IList<UserDto> GetUsersNotInterestedInProject(Guid projectId);

        public IList<UserDto> GetInterestedUsersByProjectId(Guid projectId);

        public IList<ProjectDto>? GetInterestedProjectsByUserId(Guid userId);
    }
}