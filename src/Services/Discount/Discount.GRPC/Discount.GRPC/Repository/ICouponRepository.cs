using Discount.GRPC.Entity;

namespace Discount.GRPC.Repository
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
