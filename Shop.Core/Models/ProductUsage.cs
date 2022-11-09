using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class ProductUsage : BaseModel
    {
        public ProductUsage()
        {
        }
        public int UsageId { get; set; }
        public int ProductId { get; set; }

    }
}
