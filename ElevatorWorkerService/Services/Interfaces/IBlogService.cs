using ElevatorWorkerService.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetBlogsAsync(CancellationToken cancellationToken = default);

        Task<Blog> GetBlogAsync(string id, CancellationToken cancellationToken = default);

        Task InsertBlogAsync(Blog blog, CancellationToken cancellationToken = default);

        Task<bool> UpdateBlogAsync(string id, Blog blog, CancellationToken cancellationToken = default);
    }
}
