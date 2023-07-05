using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Models.Interfaces
{
	internal interface IDeletableEntity
	{
		public bool IsDeleted { get; set; }
	}
}
