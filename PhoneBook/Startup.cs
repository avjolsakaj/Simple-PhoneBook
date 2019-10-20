using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PhoneBook.BLL.Implementation;
using PhoneBook.BLL.Interface;
using PhoneBook.DAL.Implementation;
using PhoneBook.DAL.Interface;

namespace PhoneBook
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // DI
            services.AddScoped<IPhoneBookService, PhoneBookService>();
            services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Phone Book API",
                    Description = "A simple Phone Book API with .net core",
                    Contact = new OpenApiContact
                    {
                        Name = "Avjol Sakaj",
                        Email = "avjolsakaj@outlook.com",
                        Url = new Uri("https://www.linkedin.com/in/avjol-sakaj/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "This is just a simple webapi for phone books create by Avjol Sakaj",
                        Url = new Uri("https://www.linkedin.com/in/avjol-sakaj/"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddApiVersioning(option =>
            {
                option.ReportApiVersions = true;
                option.ApiVersionReader = new HeaderApiVersionReader("api-version");
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Phone Book API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
