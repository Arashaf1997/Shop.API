using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<int> Add(AddCommentDto addCommentDto)
        {
            string sql = "INSERT INTO dbo.Comments(UserId,ProductId,Text,InsertTime,EditTime,ReplyTo)VALUES(@UserId, @ProductId, @Text, GETDATE(), NULL , @ReplyTo)";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, addCommentDto);
            return result;
        }

        public async Task<int> AddAsync(Comment entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            string sql = "DELETE dbo.Comments WHERE (Id = @Id OR ReplyTo = @Id)";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, new {Id = id});
            return result;
        }

        public async Task<List<GetCommentDto>> GetAllByProductId(int productId, string order = "1 desc", int pageSize = 12, int pageNumber = 1)
        {
            string sql = @$"SELECT Id,UserId,ProductId,Text,InsertTime,EditTime,ReplyTo FROM dbo.Comments WHERE ProductId = @ProductId
                            ORDER BY {order} OFFSET {pageSize * (pageNumber - 1)} ROWS FETCH NEXT {pageSize} ROWS ONLY;";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.QueryAsync<GetCommentDto>(sql,new { ProductId = productId });
            return result.ToList();
        }

        public Task<Comment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(UpdateCommentDto updateCommentDto)
        {
            string sql = "UPDATE dbo.Comments SET Text = @Text , EditTime = GETDATE() WHERE Id = @Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, updateCommentDto);
            return result;
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
