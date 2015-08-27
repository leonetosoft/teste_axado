using Aplicacao;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRUD_Axado.Controllers
{
    /// <summary>
    /// Autenticacao ....
    /// Se nao tiver autenticado redireciona pro login
    /// </summary>
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
           
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            if (ModelState.IsValid)
            {
                UsuarioAuth auth = new UsuarioAuth();

                if (auth.AutenticaUsuario(user.UserName, user.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Informações de login incorretas!");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            new UsuarioAuth().deslogar();
            return RedirectToAction("Login", "User");
        }


    }
}
