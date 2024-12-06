using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JetBrains.Annotations;
using Logic.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Resources.Models;
using Xunit;

namespace Tests.Attributes;

[TestSubject(typeof(TokenValidationAttribute))]
public class TokenValidationAttributeTest //Difficult to thoroughly test this class, but we can test the main logic.
{
	[Fact]
	public void OnActionExecuting_SetsUnauthorizedResult_WhenTokenIsMissing()
	{
		var context = new ActionExecutingContext(
		new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
		new List<IFilterMetadata>(),
		new Dictionary<string, object>(),
		new object());

		var attribute = new TokenValidationAttribute();

		attribute.OnActionExecuting(context);

		Assert.IsType<UnauthorizedResult>(context.Result);
	}

	[Fact]
	public void OnActionExecuting_SetsUnauthorizedResult_WhenTokenIsInvalid()
	{
		var context = new ActionExecutingContext(
		new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
		new List<IFilterMetadata>(),
		new Dictionary<string, object>(),
		new object());

		context.HttpContext.Request.Headers["Authorization"] = "Bearer invalid_token";

		var attribute = new TokenValidationAttribute();

		attribute.OnActionExecuting(context);

		Assert.IsType<UnauthorizedResult>(context.Result);
	}

	[Fact]
	public void OnActionExecuting_SetsSimplifiedUser_WhenTokenIsValid()
	{
		var context = new ActionExecutingContext(
		new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
		new List<IFilterMetadata>(),
		new Dictionary<string, object>(),
		new object());

		var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
		claims: new[] { new Claim(ClaimTypes.NameIdentifier, "1"), new Claim(ClaimTypes.Role, "Seller") }));

		context.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

		var attribute = new TokenValidationAttribute();

		attribute.OnActionExecuting(context);

		var simplifiedUser = context.HttpContext.Items["SimplifiedUser"] as SimpleUser;

		Assert.NotNull(simplifiedUser);
		Assert.Equal(1, simplifiedUser.UserId);
		Assert.Equal("Seller", simplifiedUser.UserRole.ToString());
	}

	[Fact]
	public void OnActionExecuting_SetsUnauthorizedResult_WhenTokenIsMissingUserIdClaim()
	{
		var context = new ActionExecutingContext(
		new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
		new List<IFilterMetadata>(),
		new Dictionary<string, object>(),
		new object());

		var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
		claims: new[] { new Claim(ClaimTypes.Role, "Seller") })); //Missing NameIdentifier claim

		context.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

		var attribute = new TokenValidationAttribute();

		attribute.OnActionExecuting(context);

		Assert.IsType<UnauthorizedResult>(context.Result);
	}
}