using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Logic.Utilities;
using Xunit;

namespace Tests.Utilities;

[TestSubject(typeof(JwtGenerator))]
public class JwtGeneratorTest
{
	private string GetMockKey()
	{
		return "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef"; // 32 characters
	}

	[Fact]
	public void GenerateToken_ReturnsValidToken()
	{
		JwtGenerator.Key = GetMockKey();
		string token = JwtGenerator.GenerateToken(1, "test@example.com", true);
		Assert.False(string.IsNullOrEmpty(token));
	}

	[Fact]
	public void GenerateToken_ThrowsExceptionForNullKey()
	{
		JwtGenerator.Key = null;
		Assert.Throws<Exception>(() => JwtGenerator.GenerateToken(1, "test@example.com", true));
	}

	[Fact]
	public void GenerateToken_ThrowsExceptionForDefaultKey()
	{
		JwtGenerator.Key = "blank";
		Assert.Throws<Exception>(() => JwtGenerator.GenerateToken(1, "test@example.com", true));
	}

	[Fact]
	public void GenerateToken_ContainsCorrectClaims()
	{
		JwtGenerator.Key = GetMockKey();
		string token = JwtGenerator.GenerateToken(1, "test@example.com", true);
		var tokenHandler = new JwtSecurityTokenHandler();
		var jwtToken = tokenHandler.ReadJwtToken(token);
		var claims = jwtToken.Claims.ToList();
		Assert.Contains(claims, c => c.Type == ClaimTypes.NameIdentifier && c.Value == "1");
		Assert.Contains(claims, c => c.Type == ClaimTypes.Email && c.Value == "test@example.com");
		Assert.Contains(claims, c => c.Type == ClaimTypes.Role && c.Value == "Seller");
	}

	[Fact]
	public void GenerateToken_ContainsCorrectExpiration()
	{
		JwtGenerator.Key = GetMockKey();
		string token = JwtGenerator.GenerateToken(1, "test@example.com", true, 1);
		var tokenHandler = new JwtSecurityTokenHandler();
		var jwtToken = tokenHandler.ReadJwtToken(token);
		Assert.True(jwtToken.ValidTo <= DateTime.UtcNow.AddDays(1));
	}
}