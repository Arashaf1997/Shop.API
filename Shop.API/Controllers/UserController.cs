using Application.Interfaces;
using Dependencies.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dtos.UserDtos;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //Unit of work to give access to the repositories
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            // Inject Unit Of Work to the contructor of the user controller
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllPaged")]
        public async Task<IActionResult> GetAllPaged(string order = "1 desc", int pageSize = 12, int pageNumber = 1)
        {
            //var data = await _unitOfWork.Users.GetAllPagedAsync(order, pageSize, pageNumber);
            //return Ok(data);
            return Ok();
        }

        /// <summary>
        /// This endpoint returns a single user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User object</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Users.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        /// <summary>
        /// This endpoint adds a user to the database based on User model
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Status for creation</returns>
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            var data = await _unitOfWork.Users.AddAsync(user);
            return Ok(data);
        }

        [HttpPost("InviteColleague")]
        public async Task<IActionResult> InviteColleague(InviteColleagueUserDto dto)
        {
            var data = _unitOfWork.Users.InviteColleague(dto);
            if (data != 0)
                return Ok(data);
            else
                return BadRequest("User already exists");
        }

        /// <summary>
        /// This endpont deletes a user form the database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status for deletion</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Users.DeleteAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint updates a user by ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            var data = await _unitOfWork.Users.UpdateAsync(user);
            return Ok(data);
        }
    }
}