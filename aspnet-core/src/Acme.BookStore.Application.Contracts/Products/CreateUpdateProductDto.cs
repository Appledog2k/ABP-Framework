using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.Products
{
    public class CreateUpdateProductDto
    {
        public string Name { get; set; }
        [Required]
        public ProductType Type { get; set; } = ProductType.Undefined;
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public float Price { get; set; }
    }
}
