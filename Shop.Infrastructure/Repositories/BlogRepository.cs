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

namespace Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IConfiguration _configuration;
        public BlogRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Blog entity)
        {
            entity.InsertTime = DateTime.Now;
            entity.EditTime = null;

            // Basic SQL statement to insert a product into the products table
            var sql = "INSERT INTO dbo.Blog (BlogName,InsertTime,EditTime) VALUES (@BlogName,@InsertTime,@EditTime)";

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
            var sql = "DELETE FROM dbo.Blog WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM dbo.Blog WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Blog>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Blog entity)
        {
            var sql = "UPDATE dbo.ProductBlog SET BlogName = @BlogName, EditTime = GETDATE() WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { });
                return result;
            }
        }

        public async Task<IReadOnlyList<GetBlogDto>> GetAllAsync()
        {
            var sql = @"SELECT b.Id, b.Subject, b.Text, b.ImageFileContentId, b.InsertTime, b.EditTime FROM dbo.Blog b";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.QueryAsync<GetBlogDto>(sql);
            return result.ToList();
        }

        public async Task<int> Add(AddBlogDto addBlogDto)
        {
            var sql = @$"INSERT INTO dbo.Blog(Subject,Text,ImageFileContentId,InsertTime,EditTime)VALUES(@Subject,@Text,@ImageFileContentId,GETDATE(),NULL)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, new { Subject = addBlogDto.Subject, Text = addBlogDto.Text, ImageFileContentId = addBlogDto.ImageFileContentId }) ;
            return result;
        }

        public async Task<int> Update(UpdateBlogDto updateBlogDto)
        {
            var sql = "UPDATE dbo.Blog SET Subject = @Subject , Text = @Text, ImageFileContentId = @ImageFileContentId ,EditTime = GETDATE() WHERE Id = @Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, updateBlogDto);
            return result;
        }
    }
}
