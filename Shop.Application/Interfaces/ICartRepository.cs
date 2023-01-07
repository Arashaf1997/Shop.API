using Dependencies.Models;
using Shop.Application.Dtos.CartDtos;

namespace Application.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Task<IReadOnlyList<GetCartDto>> GetByUserIdAsync(int userId);
        public Task<int> AddItemToCart(AddItemToCartDto addItemToCartDto);
        public Task<int> RemoveCartItem(RemoveItemFromCartDto removeItemFromCartDto);
        public Task<int> AddItemsListToCart(List<AddItemToCartDto> addItemToCartDtos);
    }
}