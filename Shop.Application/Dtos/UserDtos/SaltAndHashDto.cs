using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.UserDtos
{
    public class SaltAndHashDto
    {
        public byte[] PasswordHash { get; set; } 
        public byte[] PasswordSalt { get; set; } 
    }
}
