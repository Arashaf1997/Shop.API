using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.CartDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration _configuration;
        public CartRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public Task<int> AddAsync(Cart entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddItemsListToCart(List<AddItemToCartDto> addItemToCartDtos)
        {
            string sql = @$"DELETE FROM dbo.Cart WHERE UserId = {addItemToCartDtos[0].UserId} /n";
            foreach(var item in addItemToCartDtos)
            {
                sql += $@"INSERT INTO dbo.Cart (ProductColorId, UserId, Count) VALUES ({item.ProductColorId},{item.UserId},{item.Count}); /n";
            }
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql);
            return result;
        }

        public async Task<int> AddItemToCart(AddItemToCartDto addItemToCartDto)
        {
            var sql = $@"DROP TABLE IF EIXSTS #T
                        SELECT {addItemToCartDto.ProductColorId} ProductColorId,{addItemToCartDto.UserId} UserId,{addItemToCartDto.Count} Count INTO #T
                        MERGE dbo.Cart AS Target
                        USING #T AS Source
                        ON Source.ProductColorId = Target.ProductColorId AND Source.UserId = Target.UserId
                        WHEN NOT MATCHED THEN
                            INSERT (ProductColorId,UserId, Count, InsertTime) 
                            VALUES (Source.ProductColorId,Source.UserId, Source.Count, GETDATE())
                         WHEN MATCHED THEN UPDATE SET
                                Target.Count = Source.Count + Target.Count,
                        		Target.EditTime = GETDATE();";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql);
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = $@"DELETE FROM dbo.Cart WHERE UserId = {id}";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql);
            return result;
        }

        public Task<Cart> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<GetCartDto>> GetByUserIdAsync(int userId)
        {
            var sql = $@"SELECT p.Title ProductTitle, c.ProductColorId,c.Count,c.UserId,p.Id ProductId, pc.ColorId, colors.ColorName, 
                        pc.Price ItemRealPrice, pd.Price ItemDiscountedPrice, pd.DiscountPercent , 
                        ISNULL(pd.Price, pc.Price) * Count TotalItemPrice,SUM(ISNULL(pd.Price, pc.Price) * Count) OVER() TotalCartPrice, fc.GuidName + '.' + fc.FileExtension ProductImage
                         FROM dbo.Cart c
                        LEFT JOIN dbo.ProductColors pc ON c.ProductColorId = pc.Id
                        LEFT JOIN dbo.Products p ON pc.ProductId = p.Id
                        LEFT JOIN dbo.Colors colors on pc.ColorId = colors.Id
                        LEFT JOIN dbo.ProductDiscounts pd ON pd.ProductColorId = pc.Id
                        LEFT JOIN dbo.ProductImages pi ON p.Id = pi.ProductId AND pi.IsMainImage = 1
                        LEFT JOIN dbo.FileContent fc ON pi.FileContentId = fc.Id
                        WHERE c.UserId = {userId}";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.QueryAsync<GetCartDto>(sql);
            return result.ToList();
        }

        public async Task<int> RemoveCartItem(RemoveItemFromCartDto removeItemFromCartDto)
        {
            var sql = $@"DELETE FROM dbo.Cards c WHERE c.ProductColorId = {removeItemFromCartDto.ProductColorId} AND c.UserId = {removeItemFromCartDto.UserId} AND c.Count = {removeItemFromCartDto.Count}
                         UPDATE dbo.Cards SET Count = Count - {removeItemFromCartDto.Count} WHERE ProductColorId = {removeItemFromCartDto.ProductColorId} AND UserId = {removeItemFromCartDto.UserId} AND Count <> {removeItemFromCartDto.Count}";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql);
            return result;
        }

        public Task<int> UpdateAsync(Cart entity)
        {
            throw new NotImplementedException();
        }
    }
}
