using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.PropertyValueDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PropertyValuesRepository : IPropertyValuesRepository
    {
        private readonly IConfiguration _configuration;
        public PropertyValuesRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> Add(AddPropertyValueDto addPropertyValueDto)
        {
            var sql = "INSERT INTO dbo.PropertiesValues(CategoryPropertyId,Value,InsertTime,EditTime)VALUES(@CategoryPropertyId,@Value,GETDATE(),NULL)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, new { CategoryPropertyId = addPropertyValueDto.CategoryPropertyId, Value = addPropertyValueDto.Value });
            return result;
        }

        public async Task<int> AddAsync(AddPropertyValueDto addPropertyValueDto)
        {

            throw new NotImplementedException();
        }

        public Task<int> AddAsync(CategoryProperty entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(PropertyValue entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM dbo.PropertiesValues WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<List<GetPropertyValueDto>> GetAllAsync()
        {
            var sql = @" SELECT pv.Id PropertyValueId ,pv.Value,pv.CategoryPropertyId,cp.Title CategoryPropertyTitle,c.Id CategoryId, c.Title CategoryTitle FROM dbo.PropertiesValues pv
                            JOIN dbo.CategoriesProperties cp ON cp.Id = pv.CategoryPropertyId
                        	JOIN dbo.Categories c ON c.Id = cp.CategoryId Order by pv.Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.QueryAsync<GetPropertyValueDto>(sql);
            return result.ToList();
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

        public Task<int> Update(UpdatePropertyValueDto updatePropertyValueDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(CategoryProperty entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(PropertyValue entity)
        {
            throw new NotImplementedException();
        }


        Task<PropertyValue> IGenericRepository<PropertyValue>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
