using Dependencies.Models;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using Shop.Application.Dtos.ProductDtos;

namespace Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        // Make it possible to read a connection string from configuration
        private readonly IConfiguration _configuration;
        public ProductsRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Product entity)
        {
            // Set the time to the current moment
            entity.InsertTime = DateTime.Now;
            entity.EditTime = null;


            // Basic SQL statement to insert a product into the products table
            var sql = "Insert into Products (Name,Description,Barcode,Price,Added) VALUES (@Name,@Description,@Barcode,@Price,@Added)";

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
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Map all products from database to a list of type Product defined in Models.
                // this is done by using Async method which is also used on the GetByIdAsync
                var result = await connection.QueryAsync<Product>(sql);
                return result.ToList();
            }
        }
        public async Task<IReadOnlyList<GetProductDto>> GetAllPagedAsync(string order, int pageSize, int pageNumber)
        {
            var sql = @$"SELECT p.Id ProductId,
       p.Title,
       p.Description,
       p.VisitCount,
	   p.Status ProductStatus,
       cat.Title CategoryName,
       ISNULL((SELECT SUM(od.Count) FROM dbo.OrderDetails od WHERE od.ProductId = p.Id AND EXISTS (SELECT 1 FROM dbo.Orders o WHERE od.OrderId = o.Id AND o.IsPaid = 1)),0) SellCount,
       ColorPrices.Colors,
       ColorPrices.Price,
       --ColorPrices.ColleaguePrice,
       ColorPrices.DiscountPercent,
       ColorPrices.DiscountEndDate,
	(SELECT AVG(bons.Stars) StarsAvg FROM dbo.Bonuses bons WHERE bons.ProductId = p.Id) Bonus,
	 STRING_AGG(pu.UsageId,',') UsageIds,
       (
           SELECT catp.Title,
                  pv.Value
           FROM dbo.CategoriesProperties catp
               JOIN dbo.PropertiesValues pv
                   ON pv.CategoryPropertyId = catp.Id
               JOIN dbo.ProductsProperties pp
                   ON pp.ProductId = p.Id
                      AND pp.PropertyValueId = pv.Id
           WHERE catp.CategoryId = cat.Id
           FOR JSON PATH
       ) Props
FROM dbo.Products p
    JOIN dbo.Categories cat
        ON p.CategoryId = cat.Id
		LEFT JOIN dbo.ProductsUsages pu ON pu.ProductId = p.Id
    CROSS APPLY
(
    SELECT STRING_AGG(c.ColorName, ', ') Colors,
           MIN(ISNULL(pd.Price, pc.Price)) Price,
           MIN(pd.DiscountPercent) DiscountPercent,
           MIN(pd.EndDate) DiscountEndDate
    FROM dbo.ProductColors pc
        JOIN dbo.Colors c
            ON c.Id = pc.ColorId
        LEFT JOIN dbo.ProductDiscounts pd
            ON pd.ProductColorId = pc.Id
               AND GETDATE()
               BETWEEN pd.StartDate AND pd.EndDate
			   AND pd.DiscountPercent > 0
    WHERE pc.ProductId = p.Id
          AND pc.IsExists = 1
) AS ColorPrices
GROUP BY p.Id,
         cat.Id,
         p.Title,
         p.Description,
         p.VisitCount,
         cat.Title,
         ColorPrices.Colors,
         ColorPrices.Price,
         --ColorPrices.ColleaguePrice,
         ColorPrices.DiscountPercent,
         ColorPrices.DiscountEndDate,
         p.Status
ORDER BY {order} OFFSET {pageSize * (pageNumber - 1)} ROWS FETCH NEXT {pageSize} ROWS ONLY; ";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Map all products from database to a list of type Product defined in Models.
                // this is done by using Async method which is also used on the GetByIdAsync
                var result = await connection.QueryAsync<GetProductDto>(sql);
                return result.ToList();
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = @"
UPDATE dbo.Products SET VisitCount = VisitCount + 1 WHERE Id = @Id

SELECT p.Id ProductId,
       p.Title,
       p.Description,
	   p.Status ProductStatus,
       cat.Title CategoryName,
	(SELECT AVG(bons.Stars) StarsAvg FROM dbo.Bonuses bons WHERE bons.ProductId = p.Id) Bonus,
	 STRING_AGG(pu.UsageId, ',') UsageIds,
	 (
	 SELECT pc.Id ProductColorId,
            pc.ProductId,
            pc.ColorId,
			pc.Price RealPrice,
            ISNULL(pd.Price, pc.Price) DiscountedPrice,
            pc.IsExists,
			pd.DiscountPercent,
			pd.EndDate DiscountEndDate
			FROM dbo.ProductColors pc
			LEFT JOIN dbo.ProductDiscounts pd ON pd.ProductColorId = pc.Id AND GETDATE() BETWEEN pd.StartDate AND pd.EndDate
	 WHERE pc.ProductId = p.Id
	 FOR JSON PATH
	 ) ColorPrices,
       (
           SELECT catp.Title,
                  pv.Value
           FROM dbo.CategoriesProperties catp
               JOIN dbo.PropertiesValues pv
                   ON pv.CategoryPropertyId = catp.Id
               JOIN dbo.ProductsProperties pp
                   ON pp.ProductId = p.Id
                      AND pp.PropertyValueId = pv.Id
           WHERE catp.CategoryId = cat.Id
           FOR JSON PATH
       ) Props,
	   (
	   SELECT b.Stars,
              COUNT(b.UserId) UsersCount
			  FROM dbo.Bonuses b WHERE b.ProductId = p.Id
			  GROUP BY b.Stars
			  FOR JSON PATH
	   ) Bonuses
FROM dbo.Products p
    JOIN dbo.Categories cat
        ON p.CategoryId = cat.Id
        LEFT JOIN dbo.ProductsUsages pu ON pu.ProductId = p.Id
WHERE p.Id = @Id
GROUP BY p.Id,
         cat.Id,
         p.Title,
         p.Description,
         p.VisitCount,
         cat.Title,
         p.Status";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
                return result;
            }
        }
        
        public async Task<int> UpdateAsync(Product entity)
        {
            entity.EditTime = DateTime.Now;
            var sql = "UPDATE dbo.Products SET Title = @Title, Description = @Description , CategoryId = @CategoryId, Status = @Status, EditTime = GETDATE() WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

    }
}
