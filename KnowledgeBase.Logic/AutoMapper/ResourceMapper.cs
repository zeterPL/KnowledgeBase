using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.AutoMapper
{
	public class ResourceMapper : Profile
	{
		public ResourceMapper()
		{
			CreateMap<ResourceDto, Resource>();
		}
	}
}
