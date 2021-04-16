using Basket.API.Entities;
using Basket.API.gRPCServices;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountgRPCService _discountgRPCService;

        public BasketController(IBasketRepository repository, DiscountgRPCService discountgRPCService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountgRPCService = discountgRPCService;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            // TODO : Communicate with Discount.gRPC
            // and Calculate latest prices of product into shopping cart
            // consume Discount gRPC

            //TODO: Get Discount for all the items in Basket in gRPC Call.
            foreach (var item in basket.Items)
            {
                var coupon = await _discountgRPCService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket(string userName)
        {
            bool status = await _repository.DeleteBasket(userName);
            return Ok(status);
        }
    }
}
