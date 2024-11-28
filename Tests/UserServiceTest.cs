using System;
using JetBrains.Annotations;
using Logic;
using Moq;
using Resources.Interfaces.IRepository;
using Resources.Models.DbModels;
using Xunit;

namespace Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{
	[Fact]
	public void GetUserNameById_ReturnsFullName_WhenUserExists()
	{
		var mockUserRepository = new Mock<IUserRepository>();
		var userService = new UserService(mockUserRepository.Object);
		var userId = 1;
		var user = new User { Id = userId, FullName = "John Doe" };
		mockUserRepository.Setup(repo => repo.GetUserById(userId)).Returns(user);

		var result = userService.GetUserNameById(userId);

		Assert.Equal("John Doe", result);
	}

	[Fact]
	public void GetUserNameById_ReturnsNull_WhenUserDoesNotExist()
	{
		var mockUserRepository = new Mock<IUserRepository>();
		var userService = new UserService(mockUserRepository.Object);
		var userId = 1;
		mockUserRepository.Setup(repo => repo.GetUserById(userId)).Throws<InvalidOperationException>();

		var result = userService.GetUserNameById(userId);

		Assert.Null(result);
	}
}