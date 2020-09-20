using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElevatorLib.Models.Blogs;
using ElevatorWorkerService.Models.Blogs;
using ElevatorWorkerService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElevatorWorkerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // ReSharper disable once HollowTypeName
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogs()
        {
            var blogs = await _blogService.GetBlogsAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<IEnumerable<BlogDto>>(blogs));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDto>> GetBlog(string id)
        {
            var blog = await _blogService.GetBlogAsync(id).ConfigureAwait(false);
            if(blog == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BlogDto>(blog));
        }

        [HttpPost]
        public async Task<ActionResult<BlogDto>> InsertBlog([FromBody]Blog blog)
        {
            await _blogService.InsertBlogAsync(blog).ConfigureAwait(false);
            return Ok(_mapper.Map<BlogDto>(blog));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Blog>> UpdateBlog(string id, BlogUpdateModel blog)
        {
            var blogToUpdate = _mapper.Map<Blog>(blog);
            var result = await _blogService.UpdateBlogAsync(id, blogToUpdate).ConfigureAwait(false);
            if (!result) return StatusCode(StatusCodes.Status500InternalServerError);

            var updatedBlog = await _blogService.GetBlogAsync(id).ConfigureAwait(false);
            if(updatedBlog == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BlogDto>(updatedBlog));
        }
    }
}
