using ElevatorClient.Configs;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Models.Blogs;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ElevatorClient.Services
{
    public class BlogService : IBlogService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;


        public BlogService(IHttpClientFactory httpClientFactory, IOptions<BlogServiceConfiguration> options)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(options.Value.BlogHttpClientName);
        }

        public async Task<BlogDto> CreateBlog(BlogDto blog)
        {
            var response = await _httpClient.PostAsJsonAsync<BlogDto>("api/blog", blog).ConfigureAwait(false);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<BlogDto>().ConfigureAwait(false);
            }
            return null;
        }

        public async Task<BlogDto> GetBlog(string id)
        {
            var response = await _httpClient.GetAsync($"api/blog/{id}").ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlogDto>().ConfigureAwait(false);
            }
            return null;
        }

        public async Task<IEnumerable<BlogDto>> GetBlogs()
        {
            var response = await _httpClient.GetAsync("/api/blog").ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<BlogDto>>();
            }
            return null;
        }

        public async Task<BlogDto> UpdateBlog(string id, BlogDto blog)
        {
            var response = await _httpClient.PutAsync($"api/blog/{id}", JsonContent.Create<BlogDto>(blog));
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlogDto>().ConfigureAwait(false);
            }
            return null;
        }
    }
}
