using Dependencies.Models;
using Shop.Application.Dtos.BlogCategoryDtos;
using Shop.Application.Dtos.BlogDtos;
using Shop.Application.Dtos.BrandDtos;

namespace Application.Interfaces
{
    public interface IBlogCategoryRepository : IGenericRepository<BlogCategory>
    {
        public Task<IReadOnlyList<GetBlogCategoryDto>> GetAllAsync();
        public Task<int> Add(AddBlogCategoryDto addBlogDto);
    }
}