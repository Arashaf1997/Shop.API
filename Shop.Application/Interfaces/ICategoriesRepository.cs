using Dependencies.Models;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Linq;

namespace Application.Interfaces
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<IReadOnlyList<GetCategoryDto>> GetAllAsync();
        Task<int> UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task<int> Add(string title);

        //IQueryable<Category> GetFilteredCategories(string searchText);
        //IQueryable<CategoryDto> GetAllCategories();

    }
}
