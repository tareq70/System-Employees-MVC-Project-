using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using BLL.Repository;
using DAL.Dbcontext;
using DAL.Models;
using Demo.PL.Controllers;
using Demo.PL.MappingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
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
            services.AddControllersWithViews();
            services.AddDbContext<MvcAppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddAutoMapper(P=>P.AddProfiles(new List<Profile>() { new EmployeeProfile(), new UserProfile() , new RoleProfile()}));

            services.AddScoped<IunitOfWork, UnitOfWork>();

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(Option=>
            {
				Option.Password.RequireNonAlphanumeric = true;//@ # $
                Option.Password.RequireDigit = true;
                Option.Password.RequireLowercase = true;
                Option.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<MvcAppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Option=>
            {
                Option.LogoutPath = "Account/Login";
                Option.AccessDeniedPath = "Home/Error";
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });
        }
    }
}
