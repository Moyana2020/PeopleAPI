using Abstractions;
using Abstractions.Repositories;
using Abstractions.Services;
using Core.Services;
using Infrastructure;
using Infrastructure.SQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();

                });
            }
         );
            services
               .AddControllersWithViews()
               .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My API ",
                    Description = "Database Management"
                });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddApplicationInsightsTelemetry();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            string virtualDirectory = this.Configuration.GetValue<string>("AppSettings:VirtualDirectory");


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(virtualDirectory + "/swagger/v1/swagger.json", "My API");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            loggerFactory.AddSerilog();
            app.UseRouting();
            app.UseCors("CorsPolicy");

         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHealthChecks("/health");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
