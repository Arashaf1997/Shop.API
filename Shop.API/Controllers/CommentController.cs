using Application.Interfaces;
using Dependencies.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.CommentDtos;
using Shop.Application.Dtos.ProductDtos;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        //Unit of work to give access to the repositories
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork)
        {
            // Inject Unit Of Work to the contructor of the product controller
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This endpoint returns all products from the repository
        /// </summary>
        /// <returns>List of product objects</returns>
        [HttpGet("GetAllByProductIdPaged")]
        public async Task<IActionResult> GetAllByProductIdPaged(int productId, string order = "1 desc", int pageSize = 12, int pageNumber = 1)
        {
            var data = await _unitOfWork.Comments.GetAllByProductId(productId,order ,pageSize , pageNumber );
            return Ok(data);
        }

        /// <summary>
        /// This endpoint returns a single product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category object</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Comments.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        /// <summary>
        /// This endpoint adds a product to the database based on Category model
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for creation</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] AddCommentDto addCommentDto)
        {
            var data = await _unitOfWork.Comments.Add(addCommentDto);
            return Ok(data);
        }

        /// <summary>
        /// This endpont deletes a product form the database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status for deletion</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Comments.DeleteAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint updates a product by ID
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommentDto updateCommentDto)
        {
            var data = await _unitOfWork.Comments.Update(updateCommentDto);
            return Ok(data);
        }
    }
}