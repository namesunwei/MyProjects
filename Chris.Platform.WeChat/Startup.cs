using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chris.Platform.WeChat.Models.Configs;
using Chris.Platform.WeChat.Models.Middleware;
using Chris.Platform.WeChat.Services;
using Microsoft.Extensions.Logging;

namespace Chris.Platform.WeChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetSection("ServerInfos:RedisOptions:ConnectionString").Value;
                options.InstanceName = Configuration.GetSection("ServerInfos:RedisOptions:InstanceName").Value;
            });
            //Inject configs
            services.Configure<WeChatOptions>(Configuration.GetSection("WeChatOptions"));

            //Inject Services
            services.AddSingleton<WeChatServices>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseStaticFiles();

            app.UseRecordRequestInfo();

            loggerFactory.AddFile("Logs/WeChat-{Date}.txt");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
