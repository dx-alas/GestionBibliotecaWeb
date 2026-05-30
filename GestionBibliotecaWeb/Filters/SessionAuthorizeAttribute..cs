using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestionBibliotecaWeb.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = context.HttpContext.Session.GetString("Usuario");

            if (usuario == null)
            {
                context.Result = new RedirectToActionResult("Index","Login",null);
            }

            base.OnActionExecuting(context);
        }
    }
}