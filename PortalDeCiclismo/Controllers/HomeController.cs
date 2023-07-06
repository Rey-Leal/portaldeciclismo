using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalDeCiclismo.DAL;
using PortalDeCiclismo.Models;

namespace PortalDeCiclismo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            System.Web.HttpCookie cookieTipo = Request.Cookies["tipo"];
            System.Web.HttpCookie cookieIdUsuario = Request.Cookies["idUsuario"];
            System.Web.HttpCookie cookieUsuario = Request.Cookies["usuario"];
            Response.Cookies.Remove("tipo");
            Response.Cookies.Remove("idUsuario");
            Response.Cookies.Remove("usuario");
            cookieTipo = new System.Web.HttpCookie("tipo");
            cookieIdUsuario = new System.Web.HttpCookie("idUsuario");
            cookieUsuario = new System.Web.HttpCookie("usuario");

            if (ModelState.IsValid)
            {
                PortalContext db = new PortalContext();
                var usuario = (from u in db.Acessos
                               where u.Usuario == login.Usuario && u.Senha == login.Senha
                               select new
                               {
                                   u.UsuarioID,
                                   u.Usuario,
                                   u.Tipo,
                                   u.ID

                               }).ToList();
                if (usuario.FirstOrDefault() != null)
                {
                    cookieTipo.Value = usuario.FirstOrDefault().Tipo.ToString();
                    cookieTipo.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookieTipo);

                    cookieIdUsuario.Value = usuario.FirstOrDefault().ID.ToString();
                    cookieIdUsuario.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookieIdUsuario);

                    cookieUsuario.Value = usuario.FirstOrDefault().Usuario;
                    cookieUsuario.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookieUsuario);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("LoginInvalido", "Home");
                }
            }
            return RedirectToAction("LoginInvalido", "Home");
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            System.Web.HttpCookie cookieTipo = Request.Cookies["tipo"];
            System.Web.HttpCookie cookieIdUsuario = Request.Cookies["idUsuario"];
            System.Web.HttpCookie cookieUsuario = Request.Cookies["usuario"];
            Response.Cookies.Remove("tipo");
            Response.Cookies.Remove("idUsuario");
            Response.Cookies.Remove("usuario");
            cookieTipo = new System.Web.HttpCookie("tipo");
            cookieIdUsuario = new System.Web.HttpCookie("idUsuario");
            cookieUsuario = new System.Web.HttpCookie("usuario");

            cookieTipo.Value = "0";
            cookieTipo.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookieTipo);

            cookieIdUsuario.Value = "0";
            cookieIdUsuario.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookieIdUsuario);

            cookieUsuario.Value = "Login";
            cookieUsuario.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookieUsuario);

            return RedirectToAction("Login", "Home");
        }

        public ActionResult LoginInvalido()
        {
            return View();
        }
    }
}