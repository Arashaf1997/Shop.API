using Application.Interfaces;
using Dependencies.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dtos.CategoryPropertyDtos;
using Shop.Application.Dtos.ProductDtos;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryPropertyController : ControllerBase
    {
        //Unit of work to give access to the repositories
        private readonly IUnitOfWork _unitOfWork;
        public CategoryPropertyController(IUnitOfWork unitOfWork)
        {
            // Inject Unit Of Work to the contructor of the product controller
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetCategoryProperties")]
        public async Task<IActionResult> GetCategoryProperties(int categoryId)
        {
            var data = await _unitOfWork.CategoriesProperties.GetCategoryProperties(categoryId);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint returns a single product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product object</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Products.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        /// <summary>
        /// This endpoint adds a product to the database based on Product model
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for creation</returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryPropertyDto addCategoryPropertyDto)
        {
            var data = await _unitOfWork.CategoriesProperties.AddAsync(addCategoryPropertyDto);
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
            var data = await _unitOfWork.CategoriesProperties.DeleteAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint updates a product by ID
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryPropertyDto updateCategoryPropertyDto)
        {
            var data = await _unitOfWork.CategoriesProperties.UpdateAsync(updateCategoryPropertyDto);
            return Ok(data);
        }
    }
}