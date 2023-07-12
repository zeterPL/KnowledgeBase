using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Logic.Dto
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class RoleExtensions
    {
        public static RoleDto ToRoleDto(this Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }
    }
}