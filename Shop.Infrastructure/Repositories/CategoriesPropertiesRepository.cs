using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.CategoryPropertyDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class CategoriesPropertiesRepository : ICategoriesPropertiesRepository
    {
        private readonly IConfiguration _configuration;
        public CategoriesPropertiesRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(AddCategoryPropertyDto addCategoryPropertyDto)
        {

            // Basic SQL statement to insert a product into the products table
            var sql = "INSERT INTO dbo.CategoriesProperties(CategoryId,Title,InsertTime,EditTime)VALUES(@CategoryId,@Title,GETDATE(),NULL)";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.ExecuteAsync(sql, new {CategoryId = addCategoryPropertyDto.CategoryId, Title = addCategoryPropertyDto.Title});
                return result;
            }
        }

        public Task<int> AddAsync(CategoryProperty entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM dbo.CategoriesProperties WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<GetCategoryDto>> GetAllAsync()
        {
            var sql = "SELECT Id,Title FROM dbo.Categories ORDER BY Id DESC";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.QueryAsync<GetCategoryDto>(sql);
                return result.ToList();
            }
        }


        public async Task<Category> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM dbo.CategoriesProperties WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = id });
                return result;
            }
        }

        public Task<List<GetCategoryPropertyDto>> GetCategoryProperties(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(UpdateCategoryPropertyDto dto)
        {
            var sql = "UPDATE dbo.CategoriesProperties SET Title = @Title , EditTime = GETDATE() WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Title = dto.Title, Id = dto.Id});
                return result;
            }
        }

        public Task<int> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(CategoryProperty entity)
        {
            throw new NotImplementedException();
        }

        Task<CategoryProperty> IGenericRepository<CategoryProperty>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
