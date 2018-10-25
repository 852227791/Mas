using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace Mas.Tool.Web
{
    [Serializable]
    public class TokenUser 
    {
        public TokenUser()
        {

        }


        public string UserId { get; set; }


        public string UserName { get; set; }


       

        public TokenUser(IHttpContextAccessor accessor)
        {
            if (accessor.HttpContext.User.Identity is ClaimsIdentity claimsIdentity && claimsIdentity.IsAuthenticated)
            {
                //foreach (var claim in claimsIdentity.Claims)
                //{
                //    this.SetValue(claim.Type, claim.Value);
                //}

                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(m => m.Type == "sub");
                var userNameClaim = claimsIdentity.Claims.FirstOrDefault(m => m.Type == "name");
                if (userIdClaim != null)
                {
                    UserId = userIdClaim.Value;
                    UserName = userNameClaim?.Value;
                    return;
                }
            }
            SetDefaultUser();
        }

        public static TokenUser GetTokenUser(IIdentity identity)
        {
            if (identity is ClaimsIdentity claimsIdentity && claimsIdentity.IsAuthenticated)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(m => m.Type == "sub");
                var orgIdClaim = claimsIdentity.Claims.FirstOrDefault(m => m.Type == "profile");
                var userNameClaim = claimsIdentity.Claims.FirstOrDefault(m => m.Type == "name");
                var openIdClaim = claimsIdentity.Claims.FirstOrDefault(m => m.Type == "openId");

                //if (userIdClaim != null)
                //    return new TokenUser(userIdClaim.Value, userNameClaim?.Value, orgIdClaim?.Value,
                //        openIdClaim?.Value);
            }
            return null;
        }

        private void SetDefaultUser()
        {
            UserId = "Anonymous";
            UserName = "Anonymous";
        }
    }
}