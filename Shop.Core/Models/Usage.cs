using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Usage : BaseModel
    {
        public Usage()
        {
        }
        public string Title { get; set; }
    }
}
