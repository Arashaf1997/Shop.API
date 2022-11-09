using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IConfiguration _configuration;
        public CategoriesRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Category entity)
        {
            // Set the time to the current moment
            entity.InsertTime = DateTime.Now;
            entity.EditTime = null;


            // Basic SQL statement to insert a product into the products table
            var sql = "INSERT INTO dbo.Categories(Title,InsertTime,EditTime)VALUES(@Title,@InsertTime,@EditTime)";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM dbo.Categories WHERE Id = @Id";
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
            var sql = "SELECT * FROM dbo.Categories WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = id });
                return result;
            }
        }


        public async Task<int> UpdateAsync(UpdateCategoryDto entity)
        {
            var sql = "UPDATE dbo.Categories SET Title = @Title, EditTime = GETDATE() WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Title = entity.Title, Id = entity.Id});
                return result;
            }
        }

        public Task<int> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
