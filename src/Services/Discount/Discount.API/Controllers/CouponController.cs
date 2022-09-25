using System.Net;
using Discount.API.Entity;
using Discount.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet("[action]/{productName}", Name = "GetCoupon")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetCoupon(string productName)
        {
            return Ok(await _couponRepository.GetDiscount(productName));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Coupon), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update([FromBody] Coupon coupon)
        {
            var existing = await _couponRepository.GetDiscountById(coupon.Id);
            if (existing.Id == 0)
                return NotFound();

            return Ok(await _couponRepository.Update(coupon));
        }

        [HttpDelete("[action]/{id:int}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _couponRepository.GetDiscountById(id);
            if (existing.Id == 0)
                return NotFound();

            return Ok(await _couponRepository.Delete(id));
        }

        [HttpPost("[action]")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Insert([FromBody] Coupon coupon)
        {
            if (string.IsNullOrWhiteSpace(coupon.ProductName) || coupon.Amount == 0)
                return BadRequest("Invalid Credentials");

            return Ok(await _couponRepository.Add(coupon));
        }
    }
}
