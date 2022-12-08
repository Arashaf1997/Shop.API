using Dependencies.Models;
using Shop.Application.Dtos.CommentDtos;
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
        Task<List<Comment>> GetAllByProductId(int productId);
        Task<int> Add(AddCommentDto addCommentDto);
        Task<int> Update(UpdateCommentDto updateCommentDto);
    }
}
