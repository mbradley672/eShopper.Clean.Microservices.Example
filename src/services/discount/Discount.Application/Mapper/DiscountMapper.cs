using AutoMapper;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mapper;

public class DiscountMapper: Profile
{
    public DiscountMapper()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}