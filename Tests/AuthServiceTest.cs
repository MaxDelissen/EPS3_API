using JetBrains.Annotations;
using Logic;
using Logic.Utilities;
using Moq;
using Resources.Interfaces.IRepository;
using Resources.Models.DbModels;
using Xunit;

namespace Tests;

[TestSubject(typeof(AuthService))]
public class AuthServiceTest
{
	[Fact]
	public void CheckUserGenerateToken_ReturnsNullForNonExistentEmail()
	{
		var userRepositoryMock = new Mock<IUserRepository>();
		userRepositoryMock.Setup(repo => repo.EmailExists(It.IsAny<string>())).Returns(false);
		var authService = new AuthService(userRepositoryMock.Object);

		string? token = authService.CheckUserGenerateToken("nonexistent@example.com", "password123");

		Assert.Null(token);
	}

	[Fact]
	public void CheckUserGenerateToken_ReturnsNullForIncorrectPassword()
	{
		var userRepositoryMock = new Mock<IUserRepository>();
		userRepositoryMock.Setup(repo => repo.EmailExists(It.IsAny<string>())).Returns(true);
		userRepositoryMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).Returns(new User { PasswordHash = "$2a$11$U/YJbqeSct1DgtZseGSCL.K4IrYDz49Pua5wi4JSnJj0rHOOdZWby" });
		var authService = new AuthService(userRepositoryMock.Object);

		string? token = authService.CheckUserGenerateToken("test@example.com", "wrongpassword");

		Assert.Null(token);
	}

	[Fact]
	public void CheckUserGenerateToken_ReturnsTokenForValidCredentials()
	{
		JwtGenerator.Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef";
		var userRepositoryMock = new Mock<IUserRepository>();
		userRepositoryMock.Setup(repo => repo.EmailExists(It.IsAny<string>())).Returns(true);
		userRepositoryMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).Returns(new User { Id = 1, Email = "test@example.com", PasswordHash = "$2a$11$U/YJbqeSct1DgtZseGSCL.K4IrYDz49Pua5wi4JSnJj0rHOOdZWby", IsSeller = true });
		var authService = new AuthService(userRepositoryMock.Object);

		string? token = authService.CheckUserGenerateToken("test@example.com", "password");

		Assert.NotNull(token);
	}

	[Fact]
	public void RegisterUser_ReturnsEmailInUseForExistingEmail()
	{
		var userRepositoryMock = new Mock<IUserRepository>();
		userRepositoryMock.Setup(repo => repo.EmailExists(It.IsAny<string>())).Returns(true);
		var authService = new AuthService(userRepositoryMock.Object);

		AuthService.RegisterResponse response = authService.RegisterUser("John Doe", "test@example.com", "password123", true);

		Assert.Equal(AuthService.RegisterResult.EmailInUse, response.Result);
	}

	[Fact]
	public void RegisterUser_ReturnsSuccessForNewUser()
	{
		JwtGenerator.Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef";
		var newEmail = "newuser@example.com";
		var userRepositoryMock = new Mock<IUserRepository>();
		var callCount = 0;
		userRepositoryMock.Setup(repo => repo.EmailExists(It.Is<string>(email => email == newEmail)))
			.Returns(() => callCount++ != 0);
		userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>()));
		var authService = new AuthService(userRepositoryMock.Object);

		AuthService.RegisterResponse response = authService.RegisterUser("John Doe", newEmail, "password123", true);

		Assert.Equal(AuthService.RegisterResult.Success, response.Result);
		Assert.NotNull(response.Token);
		Assert.Equal(2, callCount);
	}

	[Fact]
	public void RegisterUser_ReturnsEmailInUseForNewUserWithSameEmail()
	{
		JwtGenerator.Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef";
		var newEmail = "newuser@example.com";
		var userRepositoryMock = new Mock<IUserRepository>();
		userRepositoryMock.Setup(repo => repo.EmailExists(It.IsAny<string>())).Returns(false);
		userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>()));
		userRepositoryMock.Setup(repo => repo.EmailExists(It.Is<string>(email => email == newEmail))).Returns(true);
		var authService = new AuthService(userRepositoryMock.Object);

		AuthService.RegisterResponse response = authService.RegisterUser("John Doe", newEmail, "password123", true);

		Assert.Equal(AuthService.RegisterResult.EmailInUse, response.Result);
		Assert.Null(response.Token);
	}

	[Fact]
	public void RegisterUser_ReturnsFailureIfUserNotAdded()
	{
		JwtGenerator.Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef";
		var userRepositoryMock = new Mock<IUserRepository>();
		userRepositoryMock.Setup(repo => repo.EmailExists(It.IsAny<string>())).Returns(false);
		userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>()));
		userRepositoryMock.Setup(repo => repo.EmailExists(It.IsAny<string>())).Returns(false);
		var authService = new AuthService(userRepositoryMock.Object);

		AuthService.RegisterResponse response = authService.RegisterUser("John Doe", "newuser@example.com", "password123", true);

		Assert.Equal(AuthService.RegisterResult.Failure, response.Result);
		Assert.Null(response.Token);
	}
}