namespace Basket.Application.Responses;

public class ShoppingCartResponse
{
    public string UserName { get; set; }
    public List<ShoppingCartItemResponse> Items { get; set; } = new();
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }
    public ShoppingCartResponse(string userName)
    {
        UserName = userName;
    }

    public ShoppingCartResponse()
    {
        
    }
}