﻿using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers;

public class UpdateDiscountCommandHandler: IRequestHandler<UpdateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository _repository;
    private readonly IMapper _mapper;

    public UpdateDiscountCommandHandler(IDiscountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = _mapper.Map<Coupon>(request);
        await _repository.UpdateDiscount(coupon);
        var couponModel = _mapper.Map<CouponModel>(coupon);

        return couponModel;
    }
}