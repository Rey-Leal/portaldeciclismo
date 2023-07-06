using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace PortalDeCiclismo.App_Start
{
    public class ActionFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            System.Web.HttpCookie cookieTipo = filterContext.RequestContext.HttpContext.Request.Cookies["tipo"];
            System.Web.HttpCookie cookieUsuario = filterContext.RequestContext.HttpContext.Request.Cookies["usuario"];

            //Sem acesso
            filterContext.Controller.ViewBag.TipoDeUsuario = 0;            
            filterContext.Controller.ViewBag.Usuario = "Login";

            if (cookieUsuario != null && cookieTipo != null)
            {
                switch (cookieTipo.Value.ToString())
                {
                    case "1": //Atleta
                        filterContext.Controller.ViewBag.TipoDeUsuario = 1;
                        filterContext.Controller.ViewBag.Usuario = cookieUsuario.Value.ToString();
                        break;
                    case "2": //Tecnico
                        filterContext.Controller.ViewBag.TipoDeUsuario = 2;
                        filterContext.Controller.ViewBag.Usuario = cookieUsuario.Value.ToString();
                        break;
                }
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }
    }
}