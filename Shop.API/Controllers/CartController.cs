using Application.Interfaces;
using Dependencies.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.BlogDtos;
using Shop.Application.Dtos.ProductDtos;
using Shop.Application.Dtos.CartDtos;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        //Unit of work to give access to the repositories
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var data = await _unitOfWork.Cart.GetByUserIdAsync(userId);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(List<AddItemToCartDto> addItemToCartDtos)
        {
            var data = await _unitOfWork.Cart.AddItemsListToCart(addItemToCartDtos);
            return Ok(data);
        }

        /// <summary>
        /// This endpont deletes a product form the database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status for deletion</returns>
        [HttpDelete("DeleteCart")]
        public async Task<IActionResult> DeleteCartByUserId(int userId)
        {
            var data = await _unitOfWork.Cart.DeleteAsync(userId);
            return Ok(data);
        }

        [HttpDelete("RemoveCartItem")]
        public async Task<IActionResult> RemoveCartItem(RemoveItemFromCartDto removeItemFromCartDto)
        {
            var data = await _unitOfWork.Cart.RemoveCartItem(removeItemFromCartDto);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint updates a product by ID
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        public async Task<IActionResult> AddItemToCart(AddItemToCartDto addItemToCartDto)
        {
            var data = await _unitOfWork.Cart.AddItemToCart(addItemToCartDto);
            return Ok(data);
        }
    }
}