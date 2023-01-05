using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Products
{
    public interface IProductAppService : ICrudAppService<ProductDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateProductDto>
    {
    }
}
