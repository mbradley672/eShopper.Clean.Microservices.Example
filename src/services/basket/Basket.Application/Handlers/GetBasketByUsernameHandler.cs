using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class GetBasketByUsernameHandler : IRequestHandler<GetBasketByUsernameQuery,ShoppingCartResponse>
{
    private readonly IBasketRepository _repository;

    public GetBasketByUsernameHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<ShoppingCartResponse> Handle(GetBasketByUsernameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetBasket(request.UserName);
        var response = MapperExtensions.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return response;
    }
}