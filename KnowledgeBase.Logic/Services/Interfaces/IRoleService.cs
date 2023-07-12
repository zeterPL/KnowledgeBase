using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface IRoleService
    {
        public Guid Add(RoleDto role);

        public Guid Update(RoleDto role);

        public void SoftDelete(RoleDto role);

        public IEnumerable<RoleDto> GetAll();

        public RoleDto? Get(Guid id);
    }
}