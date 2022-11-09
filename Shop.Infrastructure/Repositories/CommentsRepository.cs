using Application.Interfaces;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly IConfiguration _configuration;
        public CommentsRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public Task<int> AddAsync(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Comment entity)
        {
            throw new NotImplementedException();
        }



        //public void Set(Comment comment)
        //{
        //    Comment lastComment;

        //    if (comment.ProductId != null)
        //        lastComment = Context.Comments.FirstOrDefault(q => q.ProductId == comment.ProductId && q.UserId == comment.UserId);
        //    else
        //        lastComment = new Comment();// Context.Comments.FirstOrDefault(q => q.OrderId == comment.OrderId && q.UserId == comment.UserId);


        //    ////if (lastComment != null)
        //    ////    lastComment.IsLike = comment.IsLike;
        //    //else
        //    //{
        //    //    comment.InsertTime = DateTime.Now;
        //    //    Context.Comments.Add(comment);
        //    //}
        //}

        //public IEnumerable<Comment> GetCommentsOfProducts(int productId)
        //{
        //    var productsComments = Context.Comments.Where(a => a.ProductId == productId).ToList();
        //    return productsComments;
        //}

        //public void RemoveCommentsOfObject(int? productId, int? orderId)
        //{
        //    if (productId != null)
        //        Context.Comments.RemoveRange(Context.Comments.Where(q => q.ProductId == productId));
        //    //if (orderId != null)
        //    //    Context.Comments.RemoveRange(Context.Comments.Where(q => q.OrderId == orderId));
        //}

        //public Dictionary<string, int> GetObjectComments(int objectId, bool isProduct)
        //{
        //    int likesCount = 0;
        //    int dislikesCount = 0;

        //    var counter = new Dictionary<string, int>();

        //    if (isProduct)
        //    {
        //        if (!Context.Comments.Any(op => op.ProductId == objectId))
        //        {
        //            counter.Add("LikesCount", likesCount);
        //            counter.Add("DislikesCount", dislikesCount);
        //            return counter;
        //        }
        //        //dislikesCount = Context.Comments.Where(o => o.IsLike == false && o.ProductId == objectId).Count();
        //        //likesCount = Context.Comments.Where(o => o.IsLike == true && o.ProductId == objectId).Count();
        //    }
        //    else
        //    {
        //        //if (!Context.Comments.Any(op => op.OrderId == objectId))
        //        //{
        //        //    counter.Add("LikesCount", likesCount);
        //        //    counter.Add("DislikesCount", dislikesCount);
        //        //    return counter;
        //        //}
        //        //dislikesCount = Context.Comments.Where(o => o.IsLike == false && o.OrderId == objectId).Count();
        //        //likesCount = Context.Comments.Where(o => o.IsLike == true && o.OrderId == objectId).Count();
        //    }
        //    counter.Add("LikesCount", likesCount);
        //    counter.Add("DislikesCount", dislikesCount);
        //    return counter;
        //}
    }
}
