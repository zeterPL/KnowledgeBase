using System.Collections.Generic;
using System.Linq;

namespace Email.Models
{
	public class MessageModel
	{
		public string SenderName { get; set; }
		public string ReceiverEmail { get; set; }
		public string ProjectName { get; set; }
		public IEnumerable<string> RequestedPermissions { get; set; }


		public string PermissionsToString(IEnumerable<string> permissions, string sender)
		{
			var permissionString = string.Join("\n", permissions.Select(permission => $" {permission}"));
			return $"Dear Admin add for user named: {sender} permissions:\n{permissionString}";
		}

	}
}
