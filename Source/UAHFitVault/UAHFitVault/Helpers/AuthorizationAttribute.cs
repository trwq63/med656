using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace UAHFitVault.Helpers
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Roles will contain a comma delimited list of the roles to filter on
        /// Use: [Authorization(Roles="Physician,Patient")]
        /// </summary>
        public string Roles { get; set; }

        public AuthorizationAttribute() {

        }

        public AuthorizationAttribute(string key) {
            Roles = Resources.Roles.ResourceManager.GetString(key);//[key];;
        }


        public override void OnAuthorization(AuthorizationContext filterContext) {
            try {
                //Get list of Roles
                string[] RoleArray = Roles.Split(',');

                //Check Users Role
                bool isInRoles = false;
                foreach (string Role in RoleArray) {
                    if (filterContext.HttpContext.User.IsInRole(Role)) {
                        isInRoles = true;
                    }
                }

                //If user matches one of the roles then let them through
                if (isInRoles) {
                    base.OnAuthorization(filterContext);
                }

                //Else Send User to the Login Screen
                else {
                    filterContext
                        .Result = new RedirectToRouteResult(new RouteValueDictionary{
                            {"action", "Login"},
                            {"controller", "Account"}
                        });

                }
            }
            catch (Exception) {
                filterContext
                    .Result = new RedirectToRouteResult(new RouteValueDictionary{
                            {"action", "Login"},
                            {"controller", "Account"}
                        });
            }
        }
    }
}