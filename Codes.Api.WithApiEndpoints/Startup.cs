using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Codes.Api.WithApiEndpoints.Services.Contracts;
using Codes.Api.WithApiEndpoints.Services.Implementations;
using Codes.Api.WithApiEndpoints.Validators;
using Codes.Domain.Interfaces.Repositories;
using Codes.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Codes.Api.WithApiEndpoints
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
            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CodeValidator>();
                    fv.ImplicitlyValidateRootCollectionElements = true;
                });
            
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                
                var appName = Assembly.GetEntryAssembly()?.GetName().Name ??
                              throw new ApplicationException("Error while getting assembly name");
                c.SwaggerDoc("v1", new OpenApiInfo {Title = appName, Version = "v1"});
                
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.CustomOperationIds(apiDescription =>
                    apiDescription.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
            });
            
            // FOR PRODUCTION
            /*services.AddDbContext<CodesDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PgSqlConnection"),
                    npgsqlOptions => npgsqlOptions
                        .EnableRetryOnFailure(5, TimeSpan.FromSeconds(1), null)));*/

            // FOR TESTING ONLY
            services.AddDbContext<CodesDbContext>(options =>
                options.UseInMemoryDatabase("Codes"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICodeService, CodeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Codes.Api v1");
                    c.DisplayOperationId();
                });
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}