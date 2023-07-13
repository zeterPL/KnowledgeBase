using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Logic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Guid Add(RoleDto role)
        {
            throw new NotImplementedException();
        }

        public RoleDto? Get(Guid id)
        {
            return _roleRepository.Get(id).ToRoleDto();
        }

        public IEnumerable<RoleDto> GetAll()
        {
            var roles = _roleRepository.GetAll();
            return roles.Select(r => r.ToRoleDto());
        }

        public void SoftDelete(RoleDto role)
        {
            throw new NotImplementedException();
        }

        public Guid Update(RoleDto role)
        {
            throw new NotImplementedException();
        }
    }
}