using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Data.Data
{
	public static class DbInitializer
	{
		public static void Initialize(KnowledgeDbContext context)
		{
			if (context.Roles.Any()) { return; }

			var roles = new Role[]
			{
				new Role{
					Name= UserRoles.Basic.ToString(),
					Description = "Basic user role",
				},
				new Role{
					Name= UserRoles.Admin.ToString(),
					Description = "Admin user role",
				},
				new Role{
					Name= UserRoles.SuperAdmin.ToString(),
					Description = "SuperAdmin user role",
				},
			};

			context.Roles.AddRange(roles);
			context.SaveChanges();
		}
	}
}