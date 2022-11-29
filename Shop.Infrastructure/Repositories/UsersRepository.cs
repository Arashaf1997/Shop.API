using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Application.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<int> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public string Login(LoginUserDto request)
        {
            User user = new User();
            var sql = $"SELECT PasswordHash,PasswordSalt FROM dbo.Users WHERE UserName = '{request.Username}' ";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = connection.QueryAsync<SaltAndHashDto>(sql).Result.FirstOrDefault();

                if (VerifyPasswordHash(request.Password, result.PasswordHash, result.PasswordSalt))
                {
                    string token = CreateToken(request);
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Register(RegisterUserDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            if (request.IsEmailRegister)
            {
                User user = new User();
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.Username = request.Username;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                var sql = @"INSERT INTO dbo.Users (UserName,Password,Token,EmailAddress,PhoneNumber,InBlog,InsertTime,EditTime,PasswordHash,PasswordSalt,IsActive)
                            VALUES (@Username,NULL,0,NULL,NULL,0,GETDATE(),NULL,@PasswordHash,@PasswordSalt, 1)";

                var res = connection.Execute(sql, user /*new {Username = user.Username, PassowrdHash = user.PasswordHash, PasswordSalt = user.PasswordSalt}*/);
                return (res == 1);
            }
            else
            {
                if (IsValidTokenKey(request.Username, request.TokenKey))
                {
                    var Sql = @"UPDATE dbo.Users SET IsActive = 1, EditTime = GETDATE() WHERE UserName = @UserName";

                    using var con = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
                    var respnose = con.Execute(Sql, new { UserName = request.Username });
                    return (respnose == 1);
                }
                else
                    return false;
            }
        }

        public long SendTokenForPhoneRegister(string phoneNumber)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var query = "SELECT IsActive FROM dbo.Users WHERE (UserName = @UserName OR PhoneNumber = @UserName)";
            var isExistsOrActive = con.QueryFirstOrDefault<bool?>(query, new { UserName = phoneNumber });
            if (isExistsOrActive == null)
            {
                Random random = new Random();
                var randomToken = random.NextInt64(1111, 9999);
                var sql = @"INSERT INTO dbo.Users (UserName,Password,Token,EmailAddress,PhoneNumber,InBlog,InsertTime,EditTime,PasswordHash,PasswordSalt,IsActive)
                            VALUES (@Username,NULL,@Token,NULL,@Username,0,GETDATE(),NULL,NULL,NULL, 0)";
                con.Execute(sql, new { UserName = phoneNumber, Token = randomToken });
                return randomToken;
            }
            else if ((bool)isExistsOrActive == false)
            {
                Random random = new Random();
                var randomToken = random.NextInt64(1111, 9999);
                var sql = @"UPDATE dbo.Users SET Token = @Token WHERE (UserName = @UserName OR PhoneNumber = @UserName)";
                con.Execute(sql, new { UserName = phoneNumber, Token = randomToken });
                return randomToken;
            }
            else
                return 0;

            //TODO
            //Send SMS
        }

        public Task<int> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool IsValidTokenKey(string username, int tokenKey)
        {
            var sql = @"SELECT CASE WHEN Token = @TokenKey THEN 1 ELSE 0 END Response FROM dbo.Users WHERE UserName = @UserName";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var res = connection.QueryFirstOrDefault<bool>(sql, new { UserName = username, TokenKey = tokenKey });
            return res;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(LoginUserDto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string GetMe()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
