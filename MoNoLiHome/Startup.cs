using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoNoLiHome.Model.Configuration;
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

            services.Configure<MoNoLiHomeConfig>(Configuration.GetSection("MoNoLiHomeConfig"));

            var moNoLiConfig = new MoNoLiHomeConfig();
            Configuration.GetSection("MoNoLiHomeConfig").Bind(moNoLiConfig);

            var redisInstance = ConnectionMultiplexer.Connect(string.Format($"{moNoLiConfig.Redis.Host}:{moNoLiConfig.Redis.Port}"));
            services.AddSingleton<IConnectionMultiplexer>(redisInstance);

            services.AddTransient<IRedisConnector, RedisConnector>();
            services.AddTransient<IArrivedHomeService, ArrivedHomeService>();
            services.AddTransient<IFirebaseConnector, FirebaseConnector>();
            services.AddTransient<IMotionService, MotionService>();
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
