using Dependencies.Models;
using Shop.Application.Dtos.BlogDtos;
using Shop.Application.Dtos.BrandDtos;

namespace Application.Interfaces
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        public Task<IReadOnlyList<GetCartDto>> GetAllAsync();
        public Task<int> Add(AddBlogDto addBlogDto);
        public Task<int> Update(UpdateBlogDto updateBlogDto);
        Task<List<GetBlogDto>> GetAllPagedAsync(string order, int pageSize, int pageNumber, int blogCategoryId);
    }
}