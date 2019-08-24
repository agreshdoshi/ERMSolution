using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ERM.API.Entities;
using ERM.DataAccess;
using ERM.DataAccess.Models;
using ERM.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ERM.API
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

            // Register AutoMapper. also this will change dates format to dd/MM/yyyy
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ErmsinkTable, ERMSinkTableDto>()
                .ForMember(d => d.Date, opt => opt.MapFrom(src => src.Date.Value.ToString("dd/MM/yyyy")));
            });
            IMapper mapper = config.CreateMapper();

            // It uses Singleton so that it is used without reinstantiating
            services.AddSingleton(mapper);

            // This is where database connection is made.
            // NOTE: the database connectionstring is retrieved from secrets which is not the part of solution
            services.AddDbContext<ERMSinkDbDbContext>(options => options.UseSqlServer(Configuration["ConnectionString:ERMConnectionString"]));

            // All other dependency injection
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IErmSinkTableRepository, ERMSinkTableRepository>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Information);

            // This is the sequence where secrets would be looking.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                     optional: false,
                     reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // In Dev environment use secrets.json
                builder.AddUserSecrets<Startup>();

            }
            else
            {
                // This is a cleaner way to handle errors rather than replicating in every module.
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global Exceptions");
                            logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected error occurred. Please contact administrator");
                    });
                });
            }
            app.UseMvc();
        }
    }
}
