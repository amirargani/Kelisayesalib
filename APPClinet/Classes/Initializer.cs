using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Classes
{
    public class Initializer
    {
        public static async Task Initial(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var users = new IdentityRole(roleName);
                await roleManager.CreateAsync(users);
            }
        }
    }
}
