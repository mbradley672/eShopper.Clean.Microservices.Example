using Basket.Application.Queries;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameQuery, Unit>
{
    private readonly IBasketRepository _repository;

    public DeleteBasketByUserNameHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        await _repository.DeleteBasket(request.UserName);
        return Unit.Value;
    }
}