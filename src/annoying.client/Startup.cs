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
            services.AddDatabaseStuff(Configuration);

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
        public static IServiceCollection AddDatabaseStuff(this IServiceCollection services, IConfiguration config)
        {
            var server = config["DBServer"] ?? "localhost";
            var port = config["DBPort"] ?? "1433";
            var database = config["DBDatabase"] ?? "FootballDb";
            var user = config["DBUser"] ?? "SA";
            var password = config["DBPassword"] ?? "6Tpeople";
            var connectString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";
            //var connectString = $"server={server};user id={user};password={password};database={database};";
            System.Console.WriteLine($"conn: {connectString}");
            services.AddDbContext<ClientDbContext>(options =>
            {
                options.UseSqlServer(connectString);
            });
            return services;
        }

    }
}
