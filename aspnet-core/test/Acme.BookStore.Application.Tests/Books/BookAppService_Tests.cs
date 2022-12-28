using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace Acme.BookStore.Books
{
    public class BookAppService_Tests : BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;

        public BookAppService_Tests()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
        }
        // Phương thức unit test kiểm tra xem cuốn sách x có tồn tại trong db hay chưa ?
        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            // Act 
            var result =await _bookAppService.GetListAsync(
                 new PagedAndSortedResultRequestDto()
             );
            // Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(x => x.Name == "1984");
        }
        // Phương thức kiểm tra xem tạo thành công sách chưa (sách hợp lệ)
        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            // Act 
            var result = await _bookAppService.CreateAsync(
                new CreateUpdateBookDto
                {
                    Name= "Sách 3",
                    Price= 10,
                    PublishDate = DateTime.Now,
                    Type= BookType.Fantastic
                });
            // Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("Sách 3");
        }

        // Phuowng thức kiếm tra thêm mới một quyển sách không hợp lệ và không thành công
        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            // Act
            var exception = await Assert.ThrowsAsync<AbpValidationException>(
                async () =>
                {
                    await _bookAppService.CreateAsync(
                        new CreateUpdateBookDto
                        {
                            Name = "",
                            Price = 10,
                            PublishDate = DateTime.Now,
                            Type = BookType.ScienceFiction
                        }
                    );
                });
            // Assert
            exception.ValidationErrors.ShouldContain(
                err => err.MemberNames.Any(mem => mem == "Name"));
        }
    }
}
