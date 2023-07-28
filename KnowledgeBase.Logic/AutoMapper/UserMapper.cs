using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.AutoMapper;

public class UserMapper : Profile
{
	public UserMapper()
	{
		CreateMap<User, UserDto>();
	}
}