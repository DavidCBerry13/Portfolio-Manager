using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.ApiUtil.Controllers;
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
using Framework.ApiUtil.Filters;

namespace SecuritiesApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            HostingEnvironment = env;

            this.LoggerFactory = loggerFactory;

            // For NLog                   
            env.ConfigureNLog("nlog.config");
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public ILoggerFactory LoggerFactory { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc( options =>
            {
                options.Filters.Add(new ExceptionHandlerFilterAttribute(LoggerFactory));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Data Access Configuration
            var connectionString = this.Configuration.GetConnectionString("SecuritiesDatabase");
            services.RegisterDapperDataAccessClasses(connectionString);

            services.RegisterServiceClasses();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            this.ConfigureServicesVersioning(services);
            this.ConfigureServicesSwagger(services);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            // Configure Cors
            app.UseCors(builder =>
               builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
            );

            app.UseHttpsRedirection();
            app.UseMvc();

            // Configure Swagger
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);

            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Security Data API v1");
                c.RoutePrefix = string.Empty;
            });
        }


        private void ConfigureServicesVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }


        private void ConfigureServicesSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Security Data API v1", Version = "v1" });
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

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.DescribeAllEnumsAsStrings();
                options.CustomSchemaIds(x => x.FullName);
            });
        }

    }
}
