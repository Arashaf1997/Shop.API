using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Bonus : BaseModel
    {
        public Bonus()
        {
        }
        public int Stars { get; set; }

        public int UserId { get; set; }
        
        public int ProductId { get; set; }

    }
}
