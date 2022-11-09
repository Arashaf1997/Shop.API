using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class ProductProperty : BaseModel
    {
        public ProductProperty()
        {
        }

        public int ProductId { get; set; }
        public int PropertyValueId { get; set; }

    }
}
