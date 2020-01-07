using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using BrokerageAccountApi.Data.EF;
using BrokerageAccountApi.Core;
using BrokerageAccounts.Data.Dapper;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using DavidBerry.Framework.ApiUtil;
using Microsoft.Extensions.Hosting;

namespace BrokerageAccountApi
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
            var connectionString = this.Configuration.GetConnectionString("BrokerageAccountDatabase");
            //services.RegisterEfCoreDataAccessClasses(connectionString, LoggerFactory);
            services.RegisterEfCoreDataAccessClasses(connectionString);
            services.RegisterServiceClasses();

            this.ConfigureServicesSwagger(services);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Configure Cors
            app.UseCors(builder =>
               builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configure Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brokerage Account Data API v1");
            });
        }



        private void ConfigureServicesSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Investment Account Data API v1", Version = "v1" });
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
                options.CustomSchemaIds(x => x.FullName);
            });
        }

    }
}
