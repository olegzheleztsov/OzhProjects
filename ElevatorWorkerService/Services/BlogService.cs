using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using ElevatorWorkerService.Models.Blogs;
using ElevatorWorkerService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace ElevatorWorkerService.Services
{
    public class BlogService : IBlogService
    {
        private const string CONNECTION_STRING_NAME = "BlogConnectionString";
        private readonly IMongoCollection<Blog> _blogCollection;


        // ReSharper disable once SuggestBaseTypeForParameter
        public BlogService(IConfiguration configuration, ILogger<BlogService> logger)
        {
            var connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME);
            var settings = MongoClientSettings.FromUrl(
                new MongoUrl(connectionString)
            );
            settings.RetryWrites = false;

            settings.SslSettings = new SslSettings {EnabledSslProtocols = SslProtocols.Tls12};
            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase("Blogs");
            _blogCollection = database.GetCollection<Blog>("blogs");
            logger.LogInformation($"Connection string for blog service: {connectionString}");
        }

        public async Task<Blog> GetBlogAsync(string id, CancellationToken cancellationToken = default)
        {
            var cursor = await _blogCollection.FindAsync(blog => blog.Id == id, null, cancellationToken)
                .ConfigureAwait(false);
            return await cursor.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Blog>> GetBlogsAsync(CancellationToken cancellationToken = default)
        {
            var cursor = await _blogCollection.FindAsync(blog => true, null, cancellationToken).ConfigureAwait(false);
            return await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task InsertBlogAsync(Blog blog, CancellationToken cancellationToken = default)
        {
            blog.Id = null;
            await _blogCollection.InsertOneAsync(blog, null, cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> UpdateBlogAsync(string id, Blog blog,
            CancellationToken cancellationToken = default)
        {
            blog.Id = id;
            var result = await _blogCollection
                .ReplaceOneAsync(b => b.Id == id, blog, (ReplaceOptions) null, cancellationToken)
                .ConfigureAwait(false);

            return result.ModifiedCount == 1;
        }
    }
}