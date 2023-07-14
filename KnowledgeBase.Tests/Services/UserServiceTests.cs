using FluentAssertions;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;
using Moq;

namespace KnowledgeBase.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IProjectRepository> _projectRepository;
        private readonly Mock<IUserProjectPermissionRepository> _permissionRepository;
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _projectRepository = new Mock<IProjectRepository>();
            _permissionRepository = new Mock<IUserProjectPermissionRepository>();
            _roleRepository = new Mock<IRoleRepository>();

            _userService = new UserService(_userRepository.Object, _projectRepository.Object,
                _permissionRepository.Object, _roleRepository.Object);
        }

        [Fact]
        public void Get_UserExists_ReturnsUserDto()
        {
            Guid userId = Guid.NewGuid();
            User user = new User();
            UserDto userDto = new UserDto();

            _userRepository.Setup(u => u.Get(userId)).Returns(user);

            var result = _userService.GetById(userId);

            _userRepository.Verify(u => u.Get(userId), Times.Once());
            result.Should().Be(userDto).And.NotBeNull();
        }

        [Fact]
        public void Get_UserNotExists_ReturnsNull()
        {
            Guid userId = Guid.NewGuid();

            _userRepository.Setup(u => u.Get(userId)).Returns((User?)null);

            var result = _userService.GetById(userId);

            _userRepository.Verify(u => u.Get(userId), Times.Once());
            result.Should().BeNull();
        }

        [Fact]
        public void Update_UserExistsInDatabase_ReturnsUserDto()
        {
            UserDto userDto = new UserDto { Id = Guid.NewGuid() };
            User user = new User();

            _userRepository.Setup(u => u.Get(userDto.Id)).Returns(user);
            
            var result = _userService.Update(userDto);
          
            _userRepository.Verify(u => u.Get(userDto.Id), Times.Once());
            result.Should().Be(userDto).And.NotBeNull();
        }

        [Fact]
        public void GetPermissions_PermissionsNotExists_ReturnsNull()
        {
            Guid userId = Guid.NewGuid();
            _userRepository.Setup(u => u.GetAllUserPermissionsByUserId(userId)).Returns((IList<UserProjectPermission>?)null);

            var result = _userService.GetAllUserPermissions(userId);
            
            _userRepository.Verify(u=> u.GetAllUserPermissionsByUserId(userId), Times.Once());
            result.Should().BeNull();
        }
    }
}