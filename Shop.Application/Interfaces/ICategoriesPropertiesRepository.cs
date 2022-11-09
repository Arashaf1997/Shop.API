using Dependencies.Models;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.CategoryPropertyDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Linq;

namespace Application.Interfaces
{
    public interface ICategoriesPropertiesRepository : IGenericRepository<CategoryProperty>
    {
        Task<int> AddAsync(AddCategoryPropertyDto addCategoryPropertyDto);
        Task<int> UpdateAsync(UpdateCategoryPropertyDto updateCategoryPropertyDto);
        Task<List<GetCategoryPropertyDto>> GetCategoryProperties(int categoryId);

        //IQueryable<Category> GetFilteredCategories(string searchText);
        //IQueryable<CategoryDto> GetAllCategories();

    }
}
