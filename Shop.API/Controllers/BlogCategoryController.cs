using Application.Interfaces;
using Dependencies.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.BlogDtos;
using Shop.Application.Dtos.ProductDtos;
using Shop.Application.Dtos.BlogCategoryDtos;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    public class BlogCategoryController : ControllerBase
    {
        //Unit of work to give access to the repositories
        private readonly IUnitOfWork _unitOfWork;
        public BlogCategoryController(IUnitOfWork unitOfWork)
        {
            // Inject Unit Of Work to the contructor of the product controller
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This endpoint returns all products from the repository
        /// </summary>
        /// <returns>List of product objects</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.BlogCategory.GetAllAsync();
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
            var data = await _unitOfWork.BlogCategory.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        /// <summary>
        /// This endpoint adds a product to the database based on Category model
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for creation</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] AddBlogCategoryDto addBlogCategoryDto)
        {
            var data = await _unitOfWork.BlogCategory.Add(addBlogCategoryDto);
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
            var data = await _unitOfWork.BlogCategory.DeleteAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint updates a product by ID
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        public async Task<IActionResult> Update(BlogCategory blogCategory)
        {
            var data = await _unitOfWork.BlogCategory.UpdateAsync(blogCategory);
            return Ok(data);
        }
    }
}