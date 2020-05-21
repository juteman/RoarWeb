using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SilentRoar.Data;
using Microsoft.AspNetCore.Identity;

namespace SilentRoar
{
  
    public class Startup
    {
        private string _adminName = null;
        private string _password = null;
        private string _salt = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _adminName = Configuration["AdminName"];
            _password = Configuration["AdminPassword"];
            _salt = Configuration["Salt"];
            services.AddControllersWithViews();

            // addDbcontext 服务注册
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Reforge"))
            );

          
            // addIdentity 服务注册
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // 登录cookie
            services.ConfigureApplicationCookie(configure =>
            {
                configure.Cookie.HttpOnly = true;
                configure.ExpireTimeSpan = TimeSpan.FromDays(10);
                configure.Cookie.Name = "Login.Cookie";
                configure.LoginPath = "/Home/PrivateError";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateRoles(services).Wait();
        }

        /// <summary>
        /// 增加admin 权限
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = 
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = 
                serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            
            //here in this line we are adding Admin Role
            var roleCheck = await roleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //here in this line we are creating admin role and seed it to the database
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //here we are assigning the Admin role to the User that we have registered above 
            //Now, we are assinging admin role to this user("Ali@gmail.com"). When will we run this project then it will
            //be assigned to that user.
            IdentityUser user = await userManager.FindByNameAsync(_adminName);
            if(user == null)
            {
                user = new IdentityUser
                {
                    UserName = _adminName,
                    PasswordHash = _salt
                };
                string passwd = _password;
                var result = await userManager.CreateAsync(user, passwd);
                if(result != null)
                {
                    if (!await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        var userResult = await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }

        }
    }
}
