using Acme.BookStore.Books;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.FluentValidation
{
    public class CreateUpdateBookDtoValidator : AbstractValidator<CreateUpdateBookDto>
    {
        public CreateUpdateBookDtoValidator()
        {
            RuleFor(x => x.Name).Length(3, 10);
            RuleFor(x => x.Price).ExclusiveBetween(0.0f, 999.0f);
        }
    }
}
