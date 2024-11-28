using System;
using JetBrains.Annotations;
using Logic.Utilities;
using Xunit;

namespace Tests.Utilities;

[TestSubject(typeof(Hasher))]
public class HasherTest
{

	[Fact]
	public void HashPassword_ReturnsNonEmptyHash()
	{
		string password = "password123";
		string hash = password.HashPassword();
		Assert.False(string.IsNullOrEmpty(hash));
	}

	[Fact]
	public void VerifyPassword_ReturnsTrueForCorrectPassword()
	{
		string password = "password123";
		string hash = password.HashPassword();
		bool result = password.VerifyPassword(hash);
		Assert.True(result);
	}

	[Fact]
	public void VerifyPassword_ReturnsFalseForIncorrectPassword()
	{
		string password = "password123";
		string wrongPassword = "wrongpassword";
		string hash = password.HashPassword();
		bool result = wrongPassword.VerifyPassword(hash);
		Assert.False(result);
	}

	[Fact]
	public void HashPassword_ThrowsExceptionForNullPassword()
	{
		string password = null;
		Assert.Throws<ArgumentNullException>(() => password.HashPassword());
	}

	[Fact]
	public void VerifyPassword_ThrowsExceptionForNullPassword()
	{
		string password = null;
		string hash = "myhash";
		Assert.Throws<ArgumentNullException>(() => password.VerifyPassword(hash));
	}

	[Fact]
	public void VerifyPassword_ThrowsExceptionForNullHash()
	{
		string password = "password123";
		string hash = null;
		Assert.Throws<ArgumentNullException>(() => password.VerifyPassword(hash));
	}
}