using Application.Interfaces;
using Dependencies.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.ProductDtos;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        //Unit of work to give access to the repositories
        private readonly IUnitOfWork _unitOfWork;
        public ColorController(IUnitOfWork unitOfWork)
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
            var data = await _unitOfWork.Colors.GetAllAsync();
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
            var data = await _unitOfWork.Colors.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        /// <summary>
        /// This endpoint adds a product to the database based on Category model
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for creation</returns>
        [HttpPost]
        public async Task<IActionResult> Add(Color color)
        {
            var data = await _unitOfWork.Colors.AddAsync(color);
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
            var data = await _unitOfWork.Colors.DeleteAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint updates a product by ID
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        public async Task<IActionResult> Update(Color color)
        {
            var data = await _unitOfWork.Colors.UpdateAsync(color);
            return Ok(data);
        }
    }
}