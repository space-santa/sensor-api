using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SensorApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
            var sqlConnectionString = Configuration.GetConnectionString("Default");
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<TemperatureContext>(options => options.UseNpgsql(sqlConnectionString));
            services.BuildServiceProvider();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<TemperatureContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            services.AddMvc(
                config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                }
            );
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
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetRequiredService<TemperatureContext>())
                    {
                        Data.DbInitializer.Initialize(context);
                    }
                }
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseCors("AllowCors");
        }
    }
}
