﻿using Microsoft.AspNetCore.Identity;

namespace KnowledgeBase.Data.Models
{
	public class Role : IdentityRole<Guid>
	{
		public string Description { get; set; }
	}
}