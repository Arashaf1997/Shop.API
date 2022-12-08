using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.BrandDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class BrandsRepository : IBrandsRepository
    {
        private readonly IConfiguration _configuration;
        public BrandsRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Brand entity)
        {
            entity.InsertTime = DateTime.Now;
            entity.EditTime = null;

            // Basic SQL statement to insert a product into the products table
            var sql = "INSERT INTO dbo.Brands (BrandName,InsertTime,EditTime) VALUES (@BrandName,@InsertTime,@EditTime)";

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
            var sql = "DELETE FROM dbo.ProductBrands WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM dbo.ProductBrands WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Brand>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Brand entity)
        {
            var sql = "UPDATE dbo.ProductBrands SET BrandName = @BrandName, EditTime = GETDATE() WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { });
                return result;
            }
        }

        public async Task<IReadOnlyList<GetBrandDto>> GetAllAsync()
        {
            var sql = "SELECT Id, Title [BrandName] FROM dbo.Brands";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.QueryAsync<GetBrandDto>(sql);
                return result.ToList();
            }
        }

        public async Task<int> Add(AddBrandDto addBrandDto)
        {
            var sql = @$"INSERT INTO dbo.ProductBrands(BrandPercent,StartDate,EndDate,Description,UserId,ProductId,InsertTime,EditTime,Price,ProductColorId)VALUES
                        (@BrandPercent, @StartDate, @EndDate, @Description, @UserId, @ProductId, GETDATE(), NULL , @Price, @ProductColorId)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, addBrandDto);
            return result;
        }

        public async Task<int> Update(UpdateBrandDto updateBrandDto)
        {
            var sql = "UPDATE dbo.ProductBrands SET BrandPercent = @BrandPercent, Price = @Price, Description = @Description , StartDate = @StartDate, EndDate = @EndDate, EditTime = GETDATE() WHERE Id = @Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, updateBrandDto);
            return result;
        }
    }
}
