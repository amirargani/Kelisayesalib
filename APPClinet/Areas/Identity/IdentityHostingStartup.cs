using System;
using APPClinet.Areas.Identity.Data;
using APPClinet.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI; // Disable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc; // Add

[assembly: HostingStartup(typeof(APPClinet.Areas.Identity.IdentityHostingStartup))]
namespace APPClinet.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<APPClinetDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("APPClinetDbContextConnection")));

                //    services.AddDefaultIdentity<ApplicationUser>(options =>
                //    {
                //        options.SignIn.RequireConfirmedAccount = false; // Edit
                //            options.Password.RequireLowercase = false; // Add
                //            options.Password.RequireUppercase = false; // Add
                //            options.Password.RequireNonAlphanumeric = false; // Add
                //            options.Password.RequiredLength = 8; // Add
                //            options.SignIn.RequireConfirmedEmail = true; // Add
                //        })
                //        .AddEntityFrameworkStores<APPClinetDbContext>();
                //});
                services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false; // Edit
                    options.Password.RequireLowercase = false; // Add
                    options.Password.RequireUppercase = false; // Add
                    options.Password.RequireNonAlphanumeric = false; // Add
                    options.Password.RequiredLength = 8; // Add
                    options.SignIn.RequireConfirmedEmail = true; // Add
                    options.User.RequireUniqueEmail = true; // Add
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Add
                })
              .AddDefaultTokenProviders()
              /*.AddDefaultUI()*/ // Razor Page (Areas/Identity)
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<APPClinetDbContext>();
                services.AddAuthorization();
                services.AddAuthentication();
                services.AddRazorPages();
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
                services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Latest);
            });
        }
    }
}