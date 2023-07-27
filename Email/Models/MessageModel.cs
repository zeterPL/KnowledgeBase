using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Models
{
	public class MessageModel
	{
		public string SenderName { get; set; }
		public string ReceiverEmail { get; set; }
		public string ProjectName { get; set; }
		public IEnumerable<string> RequestedPermissions { get; set; }
	}
}
