using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Authors
{
    public interface IAuthorRepository : IRepository<Author,Guid>
    {
        Task<Author> FindByNameAsync(string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);
        Task<List<Author>> GetListAsync(
              int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null,
            bool includeDetails = false,
    CancellationToken cancellationToken = default);
    }
}