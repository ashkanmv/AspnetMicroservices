using AutoMapper;
using Dapper;
using Discount.GRPC;
using Discount.GRPC.Entity;
using Discount.GRPC.Repository;
using Grpc.Core;
using Npgsql;

namespace Discount.GRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly IDiscountRepository _couponRepository;
        private readonly IMapper _mapper;
        public DiscountService(ILogger<DiscountService> logger, IDiscountRepository couponRepository, IMapper mapper)
        {
            _logger = logger;
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetCoupon(GetCouponRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound,$"coupon not fount with the name of : {request.ProductName}"));
            _logger.LogInformation($"coupon with the name of {coupon.ProductName} and amount of {coupon.Amount} found");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<Response> CreateCoupon(CreateCouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var response = await _couponRepository.Add(coupon);
            return new Response()
            {
                Successful = response
            };
        }

        public override async Task<Response> UpdateCoupon(UpdateCouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var response = await _couponRepository.Update(coupon);
            return new Response()
            {
                Successful = response
            };
        }

        public override async Task<Response> DeleteCoupon(DeleteCouponRequest request, ServerCallContext context)
        {
            var response = await _couponRepository.Delete(request.ProductId);

            return new Response()
            {
                Successful = response
            };
        }
    }
}