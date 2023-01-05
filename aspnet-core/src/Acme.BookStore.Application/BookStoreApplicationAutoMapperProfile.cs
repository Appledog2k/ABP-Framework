using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Products;
using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();

        CreateMap<Product, ProductDto>();
        CreateMap<CreateUpdateProductDto, Product>();

        CreateMap<Author,AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
    }
}
