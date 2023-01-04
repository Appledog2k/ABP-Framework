using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.Books
{
    public class CreateUpdateBookDto 
        //: IValidatableObject
    {
  
        public string Name { get; set; }
        //[Required]
        //[StringLength(1000)]
        //public string Description { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        public float Price { get; set; }
        public Guid AuthorId { get; set; }
        //public string ConcurrencyStamp { get; set; }


        // Implement validate object thực hiện logic xác thực
        // trong trường hợp này nếu trường name và trường description có giá trị giống nhau thì sẽ trả về exception
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Name == Description)
        //    {
        //        yield return new ValidationResult(
        //            "Name and Description can not be the same!",
        //            new[] { "Name", "Description" }
        //        );
        //    }

        //}
    }
}
