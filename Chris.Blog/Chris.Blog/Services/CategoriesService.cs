using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chris.Blog.Models.Configs;
using Chris.Blog.Models.Extensions;
using Chris.Blog.Services.DbModels;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Chris.Blog.Services
{
    public class CategoriesService : MyBlogDbContext
    {
        private readonly IDistributedCache _redisCache;

        public CategoriesService(IOptions<MySqlOptions> mySqlOptions, IDistributedCache redisCache) : base(mySqlOptions)
        {
            _redisCache = redisCache;
        }

        private async Task<IEnumerable<Category>> QueryAllCategories()
        {
            using (var connection = DbConnection)
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>("SELECT id, category_name as CategoryName, parent_id as ParentId ,tags FROM `blog_categories`");
                return result;
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _redisCache.GetAsync<List<Category>>("Categories");
            if (categories != null)
            {
                return categories;
            }

            categories = (List<Category>)await QueryAllCategories();

            await _redisCache.SetAsync<List<Category>>("Categories", categories, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(10)
            });

            return categories;
        }
    }
}
