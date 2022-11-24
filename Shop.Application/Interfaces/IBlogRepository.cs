using Dependencies.Models;
using Shop.Application.Dtos.BlogDtos;
using Shop.Application.Dtos.BrandDtos;

namespace Application.Interfaces
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        public Task<IReadOnlyList<GetBlogDto>> GetAllAsync();
        public Task<int> Add(AddBlogDto addBlogDto);
        public Task<int> Update(UpdateBlogDto updateBlogDto);

    }
}