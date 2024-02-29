using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class GetDiscountQueryHandler: IRequestHandler<GetDiscountQuery, CouponModel>
{
    private readonly IDiscountRepository _repository;

    public GetDiscountQueryHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _repository.GetDiscount(request.ProductName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with ProductName={request.ProductName} is not found"));
        }
        var couponModel = new CouponModel
        {
            Id = coupon.Id,
            Amount = coupon.Amount,
            Description = coupon.Description,
            ProductName = coupon.ProductName
        };

        return couponModel;
    }
}