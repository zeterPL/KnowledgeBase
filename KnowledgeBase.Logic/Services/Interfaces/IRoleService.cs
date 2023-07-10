using KnowledgeBase.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

