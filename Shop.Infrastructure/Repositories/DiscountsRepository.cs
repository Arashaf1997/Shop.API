using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.DiscountDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class DiscountsRepository : IDiscountsRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountsRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Discount entity)
        {
            entity.InsertTime = DateTime.Now;
            entity.EditTime = null;

            // Basic SQL statement to insert a product into the products table
            var sql = "INSERT INTO dbo.Discounts (DiscountName,InsertTime,EditTime) VALUES (@DiscountName,@InsertTime,@EditTime)";

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
            var sql = "DELETE FROM dbo.ProductDiscounts WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<Discount> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM dbo.ProductDiscounts WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Discount>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Discount entity)
        {
            var sql = "UPDATE dbo.ProductDiscounts SET DiscountName = @DiscountName, EditTime = GETDATE() WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { });
                return result;
            }
        }

        public async Task<IReadOnlyList<GetDiscountDto>> GetAllAsync()
        {
            var sql = "SELECT Id,DiscountName FROM dbo.Discounts";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.QueryAsync<GetDiscountDto>(sql);
                return result.ToList();
            }
        }

        public async Task<int> Add(AddDiscountDto addDiscountDto)
        {
            var sql = @$"INSERT INTO dbo.ProductDiscounts(DiscountPercent,StartDate,EndDate,Description,UserId,ProductId,InsertTime,EditTime,Price,ProductColorId)VALUES
                        (@DiscountPercent, @StartDate, @EndDate, @Description, @UserId, @ProductId, GETDATE(), NULL , @Price, @ProductColorId)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, addDiscountDto);
            return result;
        }

        public async Task<int> Update(UpdateDiscountDto updateDiscountDto)
        {
            var sql = "UPDATE dbo.ProductDiscounts SET DiscountPercent = @DiscountPercent, Price = @Price, Description = @Description , StartDate = @StartDate, EndDate = @EndDate, EditTime = GETDATE() WHERE Id = @Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, updateDiscountDto);
            return result;
        }
    }
}
