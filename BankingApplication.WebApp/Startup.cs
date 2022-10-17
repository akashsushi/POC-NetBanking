using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.BusinessLayer.Implementation;
using BankingApplication.DataLayer.Contracts;
using BankingApplication.EFLayer.Implementations;
using BankingApplication.EFLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp
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

            //Config IManagerRepository
            services.AddTransient<IManagerRepository, ManagerRepositoryEFImpl>();

            //Config IEmployeeManager
            services.AddTransient<IEmployeeManager, EmployeeManager>();

            //Config ICustomerRepository
            services.AddTransient<ICustomerRepository, CustomerRepositoryEFImpl>();

            //Config ICustomerManager
            services.AddTransient<ICustomerManager, CustomerManager>();

            //Config IAccountRepository
            services.AddTransient<IAccountRepository, AccountRepositoryEFImpl>();

            //Config IAccountManager
            services.AddTransient<IAccountManager, AccountManager>();

            //Config ITransactionRepository
            services.AddTransient<ITransactionRepository, TransactionRepositoryEFImpl>();

            //Config ITransactionManager
            services.AddTransient<ITransactionManager, TransactionManager>();

            //Confirgure Session
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            }
            );

            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
