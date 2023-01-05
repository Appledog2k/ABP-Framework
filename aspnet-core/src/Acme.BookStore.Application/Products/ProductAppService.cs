using Acme.BookStore.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Products
{
    public class ProductAppService : CrudAppService<
        Product,
        ProductDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto>,
        IProductAppService
    {
        public ProductAppService(IRepository<Product, Guid> repository) : base(repository)
        {
            GetPolicyName = BookStorePermissions.Products.Default;
            GetListPolicyName = BookStorePermissions.Products.Default;
            CreatePolicyName = BookStorePermissions.Products.Create;
            UpdatePolicyName = BookStorePermissions.Products.Edit;
            DeletePolicyName = BookStorePermissions.Products.Delete;
        }
    }
}
