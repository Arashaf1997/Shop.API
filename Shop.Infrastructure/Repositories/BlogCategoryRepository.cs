using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.BlogDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Application.Interfaces;
using Shop.Application.Dtos.BlogCategoryDtos;

namespace Infrastructure.Repositories
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {
        private readonly IConfiguration _configuration;
        public BlogCategoryRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(BlogCategory entity)
        {
            entity.InsertTime = DateTime.Now;
            entity.EditTime = null;

            var sql = "INSERT INTO dbo.BlogsCategories (Title,InsertTime,EditTime) VALUES (@Title,@InsertTime,@EditTime)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM dbo.BlogCategory WHERE Id = @Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result;
        }

        public async Task<BlogCategory> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM dbo.BlogsCategories WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<BlogCategory>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(BlogCategory entity)
        {
            var sql = "UPDATE dbo.BlogsCategories SET Title = @Title, EditTime = GETDATE() WHERE Id = @Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, new { });
            return result;
        }

        public async Task<IReadOnlyList<GetBlogCategoryDto>> GetAllAsync()
        {
            var sql = @"SELECT bc.Id, bc.Title FROM dbo.BlogsCategories bc";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.QueryAsync<GetBlogCategoryDto>(sql);
            return result.ToList();
        }

        public async Task<int> Add(AddBlogCategoryDto addBlogCategoryDto)
        {
            var sql = "INSERT INTO dbo.BlogsCategories (Title,InsertTime,EditTime) VALUES (@Title,@InsertTime,@EditTime)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, new {Title = addBlogCategoryDto.Title } );
            return result;
        }

    }
}
