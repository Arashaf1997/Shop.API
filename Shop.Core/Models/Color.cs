using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Color  : BaseModel
    {
        public Color()
        {
        }
        public string ColorName { get; set; }

    }
}
