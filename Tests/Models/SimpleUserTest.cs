using System;
using JetBrains.Annotations;
using Resources;
using Resources.Models;
using Xunit;

namespace Tests.Models;

[TestSubject(typeof(SimpleUser))]
public class SimpleUserTest
{
	[Fact]
	public void Constructor_SetsUserIdAndRoleCorrectly_ForSeller()
	{
		var user = new SimpleUser(1, "Seller");

		Assert.Equal(1, user.UserId);
		Assert.Equal(UserRole.Seller, user.UserRole);
	}

	[Fact]
	public void Constructor_SetsUserIdAndRoleCorrectly_ForBuyer()
	{
		var user = new SimpleUser(2, "Buyer");

		Assert.Equal(2, user.UserId);
		Assert.Equal(UserRole.Buyer, user.UserRole);
	}

	[Fact]
	public void Constructor_ThrowsArgumentException_ForInvalidRole()
	{
		Assert.Throws<ArgumentException>(() => new SimpleUser(3, "InvalidRole"));
	}
}