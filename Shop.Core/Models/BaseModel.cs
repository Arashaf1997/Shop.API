﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependencies.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
        }
        public int Id { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime? EditTime { get; set; }
    }
}
