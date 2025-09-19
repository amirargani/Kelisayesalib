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
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        int _claimId;
        IApplicationUser _iuser;
        public PermissionAttribute(int claimId)
        {
            _claimId = claimId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Email
                string username = context.HttpContext.User.Identity.Name;
                _iuser = (IApplicationUser)context.HttpContext.RequestServices.GetService(typeof(IApplicationUser));
                if (!_iuser.ExistsPermission(_claimId, username))
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
