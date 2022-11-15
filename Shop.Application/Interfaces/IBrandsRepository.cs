using Dependencies.Models;
using Shop.Application.Dtos.BrandDtos;

namespace Application.Interfaces
{
    public interface IBrandsRepository : IGenericRepository<Brand>
    {
        public Task<IReadOnlyList<GetBrandDto>> GetAllAsync();
        public Task<int> Add(AddBrandDto addBrandDto);
        public Task<int> Update(UpdateBrandDto updateBrandDto);

    }
}