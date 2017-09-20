using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chris.Blog.Models.Configs;
using Chris.Blog.Services.DbModels;
using Chris.Blog.Services.DomainModels;
using Dapper;
using Microsoft.Extensions.Options;
using Snowflake;

namespace Chris.Blog.Services
{
    public class ArticlesService : MyBlogDbContext
    {
        private readonly IdWorker _idWorker;
        public ArticlesService(IOptions<MySqlOptions> mySqlOptions) : base(mySqlOptions)
        {
            _idWorker = new IdWorker(1, 1);
        }
        /// <summary>
        /// 根据ID查询单条博文
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ArticleDomain> QueryByIdAsync(long id)
        {
            using (DbConnection)
            {
                DbConnection.Open();
                var data = await DbConnection.QueryFirstOrDefaultAsync<Article>($"SELECT * FROM articles WHERE id={id}");
                return data != null ? Mapper.Map<ArticleDomain>(data) : null;
            }
        }
        /// <summary>
        /// 新增一条博文
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        public async Task<long> AddAsync(ArticleDomain domainModel)
        {
            var data = Mapper.Map<Article>(domainModel);

            data.Id = _idWorker.NextId();

            using (var connection = DbConnection)
            {
                connection.Open();

                const string sql = "INSERT INTO articles(id,categoryId,userId,title,content,createAt,modifyAt,isTop,isDisplay,hits,tags) VALUES (@id,@categoryId,@userId,@title,@content,@createAt,@modifyAt,@isTop,@isDisplay,@hits,@tags)";

                var count = await connection.ExecuteReaderAsync(sql, data);

                return data.Id;
            }
          
        }

        public async Task<ArticleDomain> ModifyByIdAsync(long id, ArticleDomain domainModel)
        {
            var data = Mapper.Map<Article>(domainModel);

            data.Id = id;

            using (var connection = DbConnection)
            {
                connection.Open();

                const string sql = "UPDATE articles SET title =@Title ,content =@Content WHERE id =@Id";

                var count = await connection.ExecuteReaderAsync(sql, data);

            }

        }


    }
}
