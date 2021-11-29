using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using annoying.client.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace annoying.client
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
            services.AddRazorPages();

            services.AddScoped<IPlayerService, PlayerService>();
            services.AddPostgresDbContext(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }

    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddPostgresDbContext(this IServiceCollection services, IConfiguration config)
        {

            //Create  postgres container
            //docker run -e POSTGRES_DB=catalogdb -e POSTGRES_USER=marcus -e POSTGRES_PASSWORD=password -p 5432:5432 --name postgres_catalog -d postgres
            //run bash to access postgres container
            //docker exec -it 480c32e2bb53 "bash" //where 480c3.. is the container id

            //psql -h localhost -p 5432 -U marcus -d catalogdb
            // \l lists dbs 
            // \c <db> connects to db 
            // \d lists db objects
            var server = config["DBServer"];// ?? "localhost";

            var port = config["DBPort"];// ?? "5432";
            var database = config["DBDatabase"];// ?? "catalogdb";


            var user = config["DBUser"];// ?? "marcus";
            var password = config["DBPassword"];// ?? "password";

            var connectionString = $"Host={server}; Port={port}; Database={database}; Username={user}; Password={password};";
            Console.WriteLine($"CONNECTION STRING Catalog: {connectionString}");

            //"User ID =postgres;Password=password;Server=localhost;Port=5432;Database=testDb;Integrated Security=true;Pooling=true;" //alternative
            services.AddDbContext<ClientDbContext>(options =>
                options.UseNpgsql(connectionString)
                       .UseSnakeCaseNamingConvention()
                    );

            return services;
        }
        // public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration config)
        // {
        //     var server = config["DBServer"] ?? "localhost";
        //     var port = config["DBPort"] ?? "1433";
        //     var database = config["DBDatabase"] ?? "FootballDb";
        //     var user = config["DBUser"] ?? "SA";
        //     var password = config["DBPassword"] ?? "6Tpeople";
        //     var connectString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";
        //     //var connectString = $"server={server};user id={user};password={password};database={database};";
        //     System.Console.WriteLine($"conn: {connectString}");
        //     services.AddDbContext<ClientDbContext>(options =>
        //     {
        //         options.UseSqlServer(connectString);
        //     });
        //     return services;
        // }

    }
}
