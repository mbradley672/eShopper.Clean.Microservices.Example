using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _repository;
    private readonly IMapper _mapper;

    public DeleteDiscountCommandHandler(IDiscountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteDiscount(request.ProductName);
        if (result)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with ProductName={request.ProductName} is not found"));
        }

        return result;
    }
}