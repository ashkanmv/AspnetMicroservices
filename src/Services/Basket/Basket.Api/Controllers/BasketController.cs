﻿using Basket.Api.Entities;
using Basket.Api.Repository.Abstraction;
using Discount.API.GRPCServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IShoppingRepository _shoppingRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(DiscountGrpcService discountGrpcService,IShoppingRepository shoppingRepository)
        {
            _discountGrpcService = discountGrpcService;
            _shoppingRepository = shoppingRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{userName}"), ActionName("GetBasket")]
        public async Task<ActionResult> GetShoppingCart(string userName)
        {
            var shoppingCart = await _shoppingRepository.GetShoppingCartAsync(userName);

            return Ok(shoppingCart ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ActionName("UpdateBasket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateShoppingCart([FromBody]ShoppingCart shoppingCart)
        {
            foreach (var item in shoppingCart.Items)
            {
                var coupoun = await _discountGrpcService.GetCouponAsync(item.ProductName);
                item.Price -= coupoun.Amount;
            }

            return Ok(await _shoppingRepository.UpdateShoppingCartAsync(shoppingCart));
        }

        [HttpDelete]
        [Route("{userName}"), ActionName("DeleteBasket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteShoppingCart(string userName)
        {
            await _shoppingRepository.DeleteShoppingCartAsync(userName);
            return Ok();
        }
    }
}
