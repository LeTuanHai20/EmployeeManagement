using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //connect dbcontext to use entity famework core
            services.AddDbContextPool<AppDbContext>(
                 options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnextion")));
            // It allows us to create, read, update and delete user accounts. 
            //Supports account confirmation, authentication, authorization,
            //password recovery, two-factor authentication with SMS. It also supports 
            //    external login providers like Microsoft, Facebook, Google etc. 
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            //revalid password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;
       
            });
            //set token after 5 hours
            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(5));
            //config to user authorization at header controller .it is the same administration controller
            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            //config of mvc
            services.AddControllersWithViews();
            //add to config to login by google
           
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("DeleteRolePolicy", 
            //        policy => policy.RequireClaim("Delete Role"));
            //});
            services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "256124415282-1psd26ecsrcvr34qnhb8timf8prtrj16.apps.googleusercontent.com";
                options.ClientSecret = "_bLdlI5SXGq7c7bniQDkla3P";
            })
            .AddFacebook(options =>
            {
                options.AppId = "292546971725655";
                options.AppSecret = "eedba671194e0248647235e5b64362e5";
            });

            //dependencies injection
            services.AddScoped<IEmployeeRespository, SQLEmployeeRepository>();  
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");
            }
            app.UseAuthentication();
           
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=index}/{id?}");

            });
        }
    }
}
