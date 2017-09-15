using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chris.Blog.Models.Configs;
using Chris.Blog.Models.MapperConfiguration;
using Chris.Blog.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chris.Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetSection("RedisOptions:ConnectionString").Value;
                options.InstanceName = Configuration.GetSection("RedisOptions:InstanceName").Value;
            });
            services.Configure<MySqlOptions>(Configuration.GetSection("MySqlOptions"));

            services.AddSingleton<CategoriesService>();
            services.AddSingleton<ArticlesService>();

            Mapper.Initialize(config =>
            {
                config.AddProfile<ArticleMapperConfiguration>();
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
