using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.BusinessLayer.Implementation;
using BankingApplication.DataLayer.Contracts;
using BankingApplication.EFLayer.Implementations;
using BankingApplication.EFLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebServices
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
            //Config Ef
            services.AddDbContext<db_bankingContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddTransient<ICustomerAsyncManager, CustomerAsyncManager>();
            services.AddTransient<ICustomerAsyncRepository, CustomerAsyncImpl>();
            services.AddTransient<IAuthorizationManager, AuthorizationManagerImpl();
            services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "V1.0",
                        Description = "API for NetBanking Services"
                    });
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "NetBankingAPI v1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
