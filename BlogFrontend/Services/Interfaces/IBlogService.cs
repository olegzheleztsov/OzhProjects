using ElevatorLib.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorClient.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDto>> GetBlogs();
        Task<BlogDto> GetBlog(string id);

        Task<BlogDto> CreateBlog(BlogDto blog);

        Task<BlogDto> UpdateBlog(string id, BlogDto blog);
    }
}
