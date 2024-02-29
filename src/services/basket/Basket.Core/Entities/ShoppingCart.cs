namespace Basket.Core.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(c => c.Price * c.Quantity);
        }
    }

    public ShoppingCart()
    {
        
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}