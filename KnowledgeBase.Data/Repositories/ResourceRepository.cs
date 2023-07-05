﻿using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Data.Repositories;

public class ResourceRepository : GenericRepository<Resource>, IGenericRepository<Resource>, IResourceRepository
{
	public ResourceRepository(KnowledgeDbContext context) : base(context)
	{
	}

	public void IsRemoved(Resource resource)
	{
		resource.IsDeleted = true;
		Update(resource);
	}
}
