using Dependencies.Models;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using Shop.Application.Dtos.ProductDtos;
using System.Data;
using System.ComponentModel;

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

        public async Task<int> Add(AddProductDto addProductDto)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var sql = @"INSERT INTO dbo.Products(Title,Description,VisitCount,UserId,CategoryId,Status,InsertTime,EditTime) OUTPUT Inserted.Id
                VALUES(@Title,@Description,0,@UserId,@CategoryId,@ProductStatus,GETDATE(),NULL)";
            var productId = await connection.QueryFirstOrDefaultAsync<int>(sql, new
            {
                Title = addProductDto.Title,
                Description = addProductDto.Description,
                UserId = 2,
                CategoryId = addProductDto.CategoryId,
                ProductStatus = addProductDto.ProductStatus
            });

            if (productId > 0)
            {
                if (addProductDto.UsageIds.Count > 0)
                {
                    foreach (var usageId in addProductDto.UsageIds)
                    {
                        sql = "INSERT INTO dbo.ProductsUsages(UsageId,ProductId,InsertTime,EditTime)VALUES(@UsageId, @ProductId , GETDATE(), NULL)";
                        await connection.ExecuteAsync(sql, new
                        {
                            UsageId = usageId,
                            ProductId = productId
                        });
                    }
                }
                int res;
                if (addProductDto.PropertyValueIds.Count > 0)
                {
                    sql = "";
                    foreach (var propertyValueId in addProductDto.PropertyValueIds)
                    {
                        if (sql == "")
                            sql = $"INSERT INTO dbo.ProductsProperties(ProductId,PropertyValueId,InsertTime,EditTime)VALUES({productId},{propertyValueId},GETDATE(),NULL)";
                        else
                            sql = sql + "; \n " + $"INSERT INTO dbo.ProductsProperties(ProductId,PropertyValueId,InsertTime,EditTime)VALUES({productId},{propertyValueId},GETDATE(),NULL)";
                    }
                    res = await connection.ExecuteAsync(sql);
                }

                if (addProductDto.ProductColors.Count > 0)
                {
                    sql = "";
                    foreach (var productColor in addProductDto.ProductColors)
                    {
                        productColor.ProductId = productId;
                        if (sql == "")
                            sql = $"INSERT INTO dbo.ProductColors(ProductId,ColorId,Price,ColleaguePrice,IsExists,InsertTime,EditTime)VALUES({productId},{productColor.ColorId},{productColor.Price}, {productColor.ColleaguePrice},{Convert.ToInt32(productColor.IsExists)},GETDATE(),NULL)";
                        else
                            sql = sql + "; \n " + $"INSERT INTO dbo.ProductColors(ProductId,ColorId,Price,ColleaguePrice,IsExists,InsertTime,EditTime)VALUES({productId},{productColor.ColorId},{productColor.Price}, {productColor.ColleaguePrice},{Convert.ToInt32(productColor.IsExists)},GETDATE(),NULL)";
                    }
                    res = connection.Execute(sql);
                }
                if (addProductDto.MainImageFileContentId > 0)
                {
                    sql = $"INSERT INTO dbo.ProductImages(ProductId,IsMainImage,FileContentId,InsertTime,EditTime)VALUES({productId},1,{addProductDto.MainImageFileContentId},GETDATE(),NULL)";
                    res = connection.Execute(sql);
                }
                if (addProductDto.ProductImageFileContentIds.Count > 0)
                {
                    sql = "";
                    foreach (var fileContentId in addProductDto.ProductImageFileContentIds)
                    {
                        if (sql == "")
                            sql = $"INSERT INTO dbo.ProductImages(ProductId,IsMainImage,FileContentId,InsertTime,EditTime)VALUES({productId},0,{fileContentId},GETDATE(),NULL)";
                        else
                            sql = sql + "; \n " + $"INSERT INTO dbo.ProductImages(ProductId,IsMainImage,FileContentId,InsertTime,EditTime)VALUES({productId},0,{fileContentId},GETDATE(),NULL)";
                    }
                    res = connection.Execute(sql);
                }
            }
            return productId;
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
	   ColorPrices.RealPrice,
       ColorPrices.DiscountedPrice,
       --ColorPrices.ColleaguePrice,
       ColorPrices.DiscountPercent,
       ColorPrices.DiscountEndDate,
	(SELECT AVG(bons.Stars) StarsAvg FROM dbo.Bonuses bons WHERE bons.ProductId = p.Id) Bonus,
	 STRING_AGG(pu.UsageId,',') UsageIds,
	 pi.Id MainImageId,
	 fc.FilePath + '\' + fc.GuidName + '_2' ImageUrl,
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
LEFT JOIN dbo.ProductImages pi ON pi.ProductId = p.Id AND pi.IsMainImage = 1
LEFT JOIN dbo.FileContent fc ON pi.FileContentId = fc.Id
    CROSS APPLY
(
    SELECT STRING_AGG(c.ColorName, ', ') Colors,
           MIN(ISNULL(pd.Price, pc.Price)) DiscountedPrice,
           MIN(pd.DiscountPercent) DiscountPercent,
           MIN(pd.EndDate) DiscountEndDate,
		   MIN(pc.Price) RealPrice
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
         ColorPrices.DiscountedPrice,
         --ColorPrices.ColleaguePrice,
         ColorPrices.DiscountPercent,
         ColorPrices.DiscountEndDate,
         p.Status,
		 ColorPrices.RealPrice,
		 pi.Id,
		 fc.GuidName,
		 fc.FilePath
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

        public static DataTable ToDataTable<T>(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
