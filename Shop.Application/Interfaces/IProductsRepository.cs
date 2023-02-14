using Dependencies.Models;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Linq;

namespace Application.Interfaces
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<GetProductDto>> GetAllPagedAsync(string order, int pageSize, int pageNumber);

        Task<int> Add(AddProductDto addProductDto);
        Task<GetSingleProductDto> GetById(int ProductId);
        Task<IReadOnlyList<GetProductDto>> GetAllPagedForColleagueAsync(string order, int pageSize, int pageNumber);

        //IQueryable<Product> GetFilteredProducts(string searchText, int? categoryId, int? userId);
        //IQueryable<GetProductDto> GetProducts(int? categoryId, int? subCategoryId, int pageSize, int pageNo, string order);
    }
}
