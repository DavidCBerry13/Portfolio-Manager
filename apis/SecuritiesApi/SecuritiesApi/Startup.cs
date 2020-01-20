using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DavidBerry.Framework.ApiUtil.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Securities.Data.Dapper;
using System.Reflection;
using System.IO;
using NLog.Web;
using Securities.Core;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using DavidBerry.Framework.ApiUtil;
using FluentValidation.AspNetCore;

namespace SecuritiesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //options.Filters.Add(new ExceptionHandlerFilterAttribute(LoggerFactory));

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddCors();
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(UrlResolver)));
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddSingleton<IConfiguration>(Configuration);

            // Data Access Configuration
            var connectionString = this.Configuration.GetConnectionString("SecuritiesDatabase");
            services.RegisterDapperDataAccessClasses(connectionString);

            services.RegisterServiceClasses();

            this.ConfigureServicesSwagger(services);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            // Configure Cors
            app.UseCors(builder =>
               builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
            );

            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Securities API v1");
            });
        }




        private void ConfigureServicesSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Security Data API v1", Version = "v1" });
                //options.DocInclusionPredicate((docName, apiDesc) =>
                //{
                //    var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                //    if (actionApiVersionModel == null)
                //    {
                //        return true;
                //    }
                //    if (actionApiVersionModel.DeclaredApiVersions.Any())
                //    {
                //        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                //    }
                //    return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                //});
                //options.AddFluentValidationRules();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.CustomSchemaIds(x => x.FullName);
            });
        }

    }
}
