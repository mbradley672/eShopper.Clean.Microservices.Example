using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Queries;

public class GetBasketByUsernameQuery: IRequest<ShoppingCartResponse>
{
    public GetBasketByUsernameQuery(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }
}