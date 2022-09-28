using Discount.GRPC;

namespace Discount.API.GRPCServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _protoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient protoServiceClient)
        {
            _protoServiceClient = protoServiceClient;
        }

        public async Task<CouponModel> GetCouponAsync(string productName)
        {
            var requst = new GetCouponRequest() { ProductName = productName };
            return await _protoServiceClient.GetCouponAsync(requst);
        }

        public async Task<bool> UpdateCouponAsync(CouponModel coupon)
        {
            var request = new UpdateCouponRequest() { Coupon = coupon };
            var response = await _protoServiceClient.UpdateCouponAsync(request);
            return response.Successful;
        }

        public async Task<bool> CreateCouponAsync(CouponModel coupon)
        {
            var request = new CreateCouponRequest() { Coupon = coupon };
            var response = await _protoServiceClient.CreateCouponAsync(request);
            return response.Successful;
        }

        public async Task<bool> DeleteCouponAsync(int productId)
        {
            var request = new DeleteCouponRequest() { ProductId = productId };
            var response = await _protoServiceClient.DeleteCouponAsync(request);
            return response.Successful;
        }
    }
}
