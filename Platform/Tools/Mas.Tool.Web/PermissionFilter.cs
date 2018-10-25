using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Mas.Tool.Web
{
    public class PermissionFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var permissionAttributes =
                ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetCustomAttributes(
                    typeof(PermissionAttribute), false);

            if (permissionAttributes.Any())
            {
                var identity = context.HttpContext.User.Identity;
                if (!identity.IsAuthenticated)
                {
                    var authenticateInfo = context.HttpContext.AuthenticateAsync("Bearer").Result;
                    if (authenticateInfo != null && authenticateInfo.Principal != null)
                    {
                        identity = authenticateInfo.Principal.Identity;
                        context.HttpContext.User = new ClaimsPrincipal(identity);
                    }
                }
                var tokenUser = TokenUser.GetTokenUser(identity);
                if (identity.IsAuthenticated && tokenUser != null)
                {
                    if (permissionAttributes.Any(m => ((PermissionAttribute)m).ModuleId == ""))
                        return;

                    //将当前用户 和 moduleId  actionValue 到授权服务器进行认证
                    var moduleActions = permissionAttributes.Select(m => m as PermissionAttribute);
                    //if (LoginedUserMenu.ValidatePermission(tokenUser.UserId, tokenUser.OrgId, moduleActions))
                    //    return;
                }

                context.Result = new StatusCodeResult(401);
            }
        }
    }
}
