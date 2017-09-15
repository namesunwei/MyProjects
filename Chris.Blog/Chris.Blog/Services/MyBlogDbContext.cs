using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Chris.Blog.Models.Configs;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Chris.Blog.Services
{
    public class MyBlogDbContext
    {
        public IDbConnection DbConnection { get; }
        public MyBlogDbContext(IOptions<MySqlOptions> mySqlOptions)
        {
            DbConnection = new MySqlConnection(mySqlOptions.Value.ConnectionString);

        }

    }
}
