using Discount.API.Entity;

namespace Discount.API.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<Coupon> GetDiscountById(int id);
        Task<bool> Update(Coupon coupon);
        Task<bool> Delete(int id);
        Task<bool> Add(Coupon coupon);
    }
}
