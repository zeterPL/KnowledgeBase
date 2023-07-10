using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Dto
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		//public string UserName { get; set; }
        public string? Password { get; set; }    
		public Guid RoleId { get; set; }
		public ICollection<PermissionName>? permissions { get; set; }
	}

	public static class UserExtensions
	{
		public static UserDto ToUserDto(this User user)
		{
			return new UserDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,			
				RoleId = user.RoleId,
			};
		}
	}
}
