using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OrdenCompra.API.Behaviours;
using OrdenCompra.API.Filters;
using OrdenCompra.API.Resolvers;
using OrdenCompra.Application;
using System;
using System.IO;
using System.Reflection;

namespace OrdenCompra.API
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
            ///////////////////////////////////////////////////////////////////////////////////////

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new BaseFirstContractResolver();
            });

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problems = new CustomBadRequest(context);

                    return new BadRequestObjectResult(problems);
                };
            });


            services.AddApplication();

            var validatorsAssembly = AppDomain.CurrentDomain.Load("OrdenCompra.Application");

            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(config => config.RegisterValidatorsFromAssembly(validatorsAssembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            ///////////////////////////////////////////////////////////////////////////////////////
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OrdenCompra.API",
                    Version = "v1",
                    Description = "API para ejecutar las operaciones del Contexto Delimitado: Orden de Compra",
                    Contact = new OpenApiContact
                    {
                        Name = "Mauro Pelaez",
                        Email = "mauro.pelaez@hotmail.es",
                        Url = new Uri("https://github.com/mpelaezv/"),
                    }
                });

                c.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrdenCompra.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
