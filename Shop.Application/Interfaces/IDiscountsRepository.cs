using Dependencies.Models;
using Shop.Application.Dtos.DiscountDtos;

namespace Application.Interfaces
{
    public interface IDiscountsRepository : IGenericRepository<Discount>
    {
        public Task<IReadOnlyList<GetDiscountDto>> GetAllAsync();
        public Task<int> Add(AddDiscountDto addDiscountDto);
        public Task<int> Update(UpdateDiscountDto updateDiscountDto);

    }
}