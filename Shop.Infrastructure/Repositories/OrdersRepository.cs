using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IConfiguration _configuration;
        public OrdersRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public Task<int> AddAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        //public IQueryable<GetAllOrderDto> GetAllOrders(int pageNo, int pageSize, string order)
        //{
        //                           //UserId = orders.UserId,
        //    var ordersQuery = (from orders in Context.Orders
        //                       select new GetAllOrderDto
        //                       {
        //                           Id = orders.Id,
        //                           InsertTime = orders.InsertTime,
        //                           TotalPrice = orders.TotalPrice,
        //                           IsPaid = orders.IsPaid
        //                       }).Skip(pageSize * (pageNo - 1)).Take(pageSize);
        //    return ordersQuery;
        //}

        //public IQueryable<Order> GetOrdersOfProduct(int? productId)
        //{
        //    if (productId != null)
        //    {
        //        if (!Context.Products.Any(q => q.Id == productId))
        //            throw new Exception();

        //        return null;
        //    }
        //    else
        //        return Context.Orders;
        //}

        //public void RemoveOrdersOfProduct(int productId)
        //{
        //    var ordersOfProduct = new Order(); //Context.Orders.Where(a => a.ProductId == productId);
        //    Context.Orders.RemoveRange(ordersOfProduct);
        //}
    }
}
