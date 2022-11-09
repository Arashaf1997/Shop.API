using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class PropertyValue : BaseModel
    {
        public PropertyValue()
        {
        }
        public int CategoryPropertyId { get; set; }
        public string Value { get; set; }
    }
}
