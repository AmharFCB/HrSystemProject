using Microsoft.AspNetCore.Mvc.Filters;

namespace MyFirstProject.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var Email = context.HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(Email))
            {
                context.HttpContext.Response.Redirect("/Account/Login");
            }

            base.OnActionExecuted(context);
        }
    }
}
