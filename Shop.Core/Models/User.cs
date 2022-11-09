using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class User : BaseModel
    {
        public User()
        {
        }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Token { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool InBlog { get; set; }

    }

    public enum UserTypes
    {
        Customer = 1,
        Colleague = 2,
        Admin = 3,
        Owner = 4,
        Support = 5
    }
}
