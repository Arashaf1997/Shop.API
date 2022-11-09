using Dependencies.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Order>
    {
        //IQueryable<Order> GetOrdersOfProduct(int? productId);
        //void RemoveOrdersOfProduct(int id);
        //IQueryable<GetAllOrderDto> GetAllOrders(int pageNo, int pageSize, string order);
    }
}
