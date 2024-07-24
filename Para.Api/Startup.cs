using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Para.Api.Middleware;
using Para.Api.Service;
using Para.Bussiness;
using Para.Bussiness.Cqrs;
using Para.Data.Context;
using Para.Data.UnitOfWork;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using FluentValidation.AspNetCore;

namespace Para.Api;

public class Startup
{
    public IConfiguration Configuration;
    
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }
    
    
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Para.Api", Version = "v1" });
        });


        var connectionStringSql = Configuration.GetConnectionString("MySqlConnection");
        services.AddDbContext<ParaDbContext>(options =>
            options.UseMySql(connectionStringSql, ServerVersion.AutoDetect(connectionStringSql),
                b => b.SchemaBehavior(MySqlSchemaBehavior.Ignore)
                      .MigrationsAssembly("Para.Api")));

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Para.Api v1"));
        }


        app.UseMiddleware<HeartbeatMiddleware>();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        app.Use((context,next) =>
        {
            if (!string.IsNullOrEmpty(context.Request.Path) && context.Request.Path.Value.Contains("favicon"))
            {
                return next();
            }
            
            var service1 = context.RequestServices.GetRequiredService<CustomService1>();
            var service2 = context.RequestServices.GetRequiredService<CustomService2>();
            var service3 = context.RequestServices.GetRequiredService<CustomService3>();

            service1.Counter++;
            service2.Counter++;
            service3.Counter++;

            return next();
        });
        
        app.Run(async context =>
        {
            var service1 = context.RequestServices.GetRequiredService<CustomService1>();
            var service2 = context.RequestServices.GetRequiredService<CustomService2>();
            var service3 = context.RequestServices.GetRequiredService<CustomService3>();

            if (!string.IsNullOrEmpty(context.Request.Path) && !context.Request.Path.Value.Contains("favicon"))
            {
                service1.Counter++;
                service2.Counter++;
                service3.Counter++;
            }

            await context.Response.WriteAsync($"Service1 : {service1.Counter}\n");
            await context.Response.WriteAsync($"Service2 : {service2.Counter}\n");
            await context.Response.WriteAsync($"Service3 : {service3.Counter}\n");
        });
    }
}
