﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.UserDtos
{
    public class GetUserDto
    {
        public int Id{ get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
