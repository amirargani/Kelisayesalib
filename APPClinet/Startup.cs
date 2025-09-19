using APPClinet.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore; // Add
using Microsoft.AspNetCore.Authentication.Cookies; // Add
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPClinet.Areas.Identity.Data.Interfaces; // Add
using APPClinet.Areas.Identity.Data.Services; // Add

namespace APPClinet
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
            // Add
            services.AddRazorPages();
            services.AddMvc();
            services.AddMvcCore();

            services.AddDbContext<APPClinetDbContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("APPClinetDbContextConnection")));

            services.AddTransient<IApplicationUser, ApplicationUserService>();


            services.AddMemoryCache();
            services.AddSession();
            // Add
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); // Edit
                //app.UseExceptionHandler("/Home/Error"); // Disable
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Add
            app.UseAuthentication();
            app.UseSession();
            app.UseCookiePolicy();
            // Add

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=fa}/{action=StartNew}/{id?}");
                // Add
                endpoints.MapRazorPages();
                // Add
            });
        }
    }
}
