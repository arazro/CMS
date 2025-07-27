using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using UserService.Application.DTOs;
using UserService.Domain.Interfaces;
using UserService.Domain.Entities;
using UserService.Tests.Helpers;
using Xunit;


namespace UserService.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Application.Services.UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new Application.Services.UserService(_userRepositoryMock.Object);
             var configuration = TestConfigurationBuilder.BuildConfiguration();
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User> {
                new User { Id = Guid.NewGuid(), Name = "Ali", Email = "ali@test.com" },
                new User { Id = Guid.NewGuid(), Name = "Zara", Email = "zara@test.com" }
            };

            _userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Ali");
        }

        [Fact]
        public async Task GetUserByIdAsync_ValidId_ReturnsUserDto()
        {
            var id = Guid.NewGuid();
            var user = new User { Id = id, Name = "Sara", Email = "sara@test.com" };
            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(user);

            var result = await _userService.GetUserByIdAsync(id);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Sara");
        }


        [Fact]
        public async Task UpdateUserAsync_ValidId_UpdatesUser()
        {
            var id = Guid.NewGuid();
            var existingUser = new User { Id = id, Name = "Old", Email = "old@test.com" };
            var updatedDto = new UserDto { Id = id, Name = "New", Email = "new@test.com" };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(r => r.UpdateAsync(existingUser)).ReturnsAsync(true);

            var result = await _userService.UpdateUserAsync(id, updatedDto);

            result.Should().BeTrue();
            existingUser.Name.Should().Be("New");
        }

        [Fact]
        public async Task DeleteUserAsync_ValidId_ReturnsTrue()
        {
            var id = Guid.NewGuid();

            _userRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _userService.DeleteUserAsync(id);

            result.Should().BeTrue();
        }

    }
}
