using Dependencies.Models;
using Shop.Application.Dtos.UserDtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        //IQueryable<User> GetFilteredUsers(string searchText);
        bool Register(RegisterUserDto request);
        string Login(LoginUserDto request);
        string GetMe();
        long SendTokenForPhoneRegister(string phoneNumber);
    }
}
