namespace Resources.Models;

public class SimpleUser
{
	public SimpleUser(int userId, string userRole)
	{
		UserId = userId;
		UserRole = userRole switch
		{
			"Seller" => UserRole.Seller,
			"Buyer" => UserRole.Buyer,
			_ => throw new ArgumentException("Invalid user role")
		};
	}

	public int UserId { get; private set; }
	public UserRole UserRole { get; private set; }
}