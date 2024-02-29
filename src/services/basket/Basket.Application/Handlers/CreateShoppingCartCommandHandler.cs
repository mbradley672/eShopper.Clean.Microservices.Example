using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _repository; 

    public CreateShoppingCartCommandHandler(IBasketRepository repository)
    {
        _repository = repository; 
    }
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        //TODO: Call Discount service to apply discount on the product price
        await _repository.UpdateBasket(new ShoppingCart(request.Username)
        {
            Items = request.Items
        });
        var resource = MapperExtensions.Mapper.Map<ShoppingCartResponse>(request);
        return resource;
    }
}