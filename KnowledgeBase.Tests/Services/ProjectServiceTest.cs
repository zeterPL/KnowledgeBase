using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Web.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.DependencyResolver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace KnowledgeBase.Tests.Services
{
    public class ProjectServiceTest
	{
        private readonly Mock<IMapper> _mapper;
		private readonly Mock<IProjectRepository> _projectRepository;
		private readonly Mock<IUserProjectPermissionRepository> _permissionRepository;
		private readonly Mock<IUserRepository> _userRepository;
		private readonly Mock<IRoleRepository> _roleRepository;
		private readonly IProjectService _projectService;

		public ProjectServiceTest()
        {
			_mapper = new Mock<IMapper>();
			_projectRepository = new Mock<IProjectRepository>();
			_permissionRepository = new Mock<IUserProjectPermissionRepository>();
			_userRepository = new Mock<IUserRepository>();
			_roleRepository = new Mock<IRoleRepository>();

			_projectService = new ProjectService(_projectRepository.Object, _permissionRepository.Object,
				_userRepository.Object, _roleRepository.Object,
				_mapper.Object);
		}

		[Fact]
		public void Poject_ReturnsExistedProject_FromDatabase()
		{
			// arrange
			var projectId = Guid.NewGuid();
			var project = new Project();
			var projectDto = new ProjectDto();

			_projectRepository.Setup(r => r.Get(projectId)).Returns(project);
			_mapper.Setup(m => m.Map<ProjectDto>(project)).Returns(projectDto);

			// act
			var result = _projectService.Get(projectId);


			// assert
			result.Equals(projectDto);
		}
	}
}
