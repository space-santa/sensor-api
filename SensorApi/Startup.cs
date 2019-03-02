using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SensorApi.Models;

namespace SensorApi
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
            string dataSource = Environment.GetEnvironmentVariable("DATA_SOURCE");

            if (dataSource == null)
            {
                dataSource = "TemperatureList.db";
            }

            System.Diagnostics.Debug.WriteLine($"Data Source={dataSource}");

            services.AddDbContext<TemperatureContext>(opt => opt.UseSqlite($"Data Source={dataSource}"));
            services.AddMvc();
            services.AddCors(
                options => options.AddPolicy("AllowCors",
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .WithMethods("GET")
                    .AllowAnyHeader();
                })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors("AllowCors");
            app.UseMvc();
        }
    }
}
