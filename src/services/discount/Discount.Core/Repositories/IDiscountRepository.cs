using System.Reflection.Metadata;
using Discount.Core.Entities;

namespace Discount.Core.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
    Task<bool> UpdateDiscount(Coupon coupon);
}

