using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Mvc;

namespace MVC_CustomRoles.CustomFilters
{
    public class AuthLogAttribute : AuthorizeAttribute
    {
        //public AuthLogAttribute()
        //{
        //    View = "AuthorizeFailed";
        //}

        public AuthLogAttribute(params string[] roleIds)
        {
            var appRoles = new List<string>();
            var roleList = (NameValueCollection)ConfigurationManager.GetSection("AppRoles");
            foreach (var roleId in roleIds)
            {
                appRoles.AddRange(roleList[roleId].Split(new[] { ',' }));
            }
            Roles = string.Join(",", appRoles);
            View = "AuthorizeFailed";
        }

        public string View { get; set; }
        
        /// <summary>
        /// Check for Authorization
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        /// <summary>
        /// Method to check if the user is Authorized or not
        /// if yes continue to perform the action else redirect to error page
        /// </summary>
        /// <param name="filterContext"></param>
        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            // If the Result returns null then the user is Authorized 
            if (filterContext.Result == null)
                return;

            //If the user is Un-Authorized then Navigate to Auth Failed View 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
               
               // var result = new ViewResult { ViewName = View };
                var vr = new ViewResult();
                vr.ViewName = View;

                ViewDataDictionary dict = new ViewDataDictionary();
                dict.Add("Message", "Sorry you are not Authorized to Perform this Action");

                vr.ViewData = dict;

                var result = vr;
                
                filterContext.Result = result;
            }
        }
    }
}
