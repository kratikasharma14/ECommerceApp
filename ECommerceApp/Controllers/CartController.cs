using ECommerceApp.DTOs.Cart;
using ECommerceApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            try
            {
                var cart = await _cartService.AddToCartAsync(dto);
                return Ok(cart);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("increase/{cartItemId}")]
        public async Task<IActionResult> Increase(int cartItemId)
        {
            try
            {
                var cart = await _cartService.IncreaseQuantityAsync(cartItemId);
                return Ok(cart);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("decrease/{cartItemId}")]
        public async Task<IActionResult> Decrease(int cartItemId)
        {
            try
            {
                var cart = await _cartService.DecreaseQuantityAsync(cartItemId);
                return Ok(cart);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            try
            {
                var cart = await _cartService.RemoveItemAsync(cartItemId);
                return Ok(cart);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}