﻿using Discount.Grpc.Protos;

namespace Basket.Application.GrpcServices;

public class DiscountService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountClient;

    public DiscountService(DiscountProtoService.DiscountProtoServiceClient discountClient)
    {
        _discountClient = discountClient;
    } 
    
    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };
        return await _discountClient.GetDiscountAsync(discountRequest);
    }
}