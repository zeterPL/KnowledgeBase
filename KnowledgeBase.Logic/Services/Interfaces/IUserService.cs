using KnowledgeBase.Logic.Dto;

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

        public void AddPermissionsByUserIdAndRoleId(Guid userId, Guid roleId);

        public void AssignPermissionBasedOnUserRole(RoleDto role, Guid userId);

        public void AddPermisionsToSpecificProject(Guid projectId, Guid userId);
    }
}