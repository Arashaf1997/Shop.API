using Dependencies.Models;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ICommentsRepository : IGenericRepository<Comment>
    {
        //IEnumerable<Comment> GetCommentsOfProducts(int productId);
        //void RemoveCommentsOfObject(int? productId, int? orderId);
        //void Set(Comment comment);
        //Dictionary<string, int> GetObjectComments(int objectId, bool isProduct);
    }
}
