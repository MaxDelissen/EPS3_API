namespace Logic;

public class ShoppingService
{
    public void AddToCart(int userId, int productId, int? quantity)
    {
        quantity ??= 1;
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0");

        //Check if user already has a cart
        //If not, create a new cart
        //Check if product is already in cart
        //If so, update quantity
        //If not, add product to cart
        //Save changes
    }
}