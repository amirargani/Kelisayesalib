using APPClinet.Areas.Identity.Data;
using APPClinet.Areas.Identity.Data.Interfaces;
using APPClinet.Areas.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Classes
{
    public class RoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        string _roleName; //string _roleId;
        IApplicationUser _iuser;
        public RoleAttribute(string roleName) // string rolrId
        {
            _roleName = roleName; //_roleId = rolrId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Email
                string username = context.HttpContext.User.Identity.Name;
                _iuser = (IApplicationUser)context.HttpContext.RequestServices.GetService(typeof(IApplicationUser));
                if (!_iuser.ExistsRoleNames(_roleName, username)) // !_iuser.ExistsRole(_roleId, username)
                {
                    _iuser.GetLogout();
                    context.Result = new RedirectResult("/Identity/Account/Login");
                }
            }
            else
            {
                _iuser.GetLogout();
                context.Result = new RedirectResult("/Identity/Account/Login");
            }
        }
    }
}
