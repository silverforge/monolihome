using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoNoLiHome.Network.Client;
using MoNoLiHome.Network.Service;
using StackExchange.Redis;

namespace MoNoLiHome
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

            var redisInstance = ConnectionMultiplexer.Connect("192.168.1.111:6379");
            services.AddSingleton<IConnectionMultiplexer>(redisInstance);

            services.AddTransient<IRedisConnector, RedisConnector>();
            services.AddTransient<IArrivedHomeService, ArrivedHomeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
