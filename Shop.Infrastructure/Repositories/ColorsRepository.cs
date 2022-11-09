using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.ColorDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class ColorsRepository : IColorsRepository
    {
        private readonly IConfiguration _configuration;
        public ColorsRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Color entity)
        {
            entity.InsertTime = DateTime.Now;
            entity.EditTime = null;

            // Basic SQL statement to insert a product into the products table
            var sql = "INSERT INTO dbo.Colors (ColorName,InsertTime,EditTime) VALUES (@ColorName,@InsertTime,@EditTime)";

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
            var sql = "DELETE FROM dbo.Colors WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<Color> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM dbo.Colors WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Color>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Color entity)
        {
            var sql = "UPDATE dbo.Colors SET ColorName = @ColorName, EditTime = GETDATE() WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { ColorName = entity.ColorName, Id = entity.Id });
                return result;
            }
        }

        public async Task<IReadOnlyList<GetColorDto>> GetAllAsync()
        {
            var sql = "SELECT Id,ColorName FROM dbo.Colors";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.QueryAsync<GetColorDto>(sql);
                return result.ToList();
            }
        }
    }
}
