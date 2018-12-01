using LuzBeer.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuzBeer.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar(string txtLogin, string txtSenha)
        {
            var xml = Util.CarregarXML();

            if(xml.Admin.Login.ToLower() == txtLogin.ToLower()
                && xml.Admin.Senha == txtSenha)
            {
                TempData["LoginUsuario"] = xml.Admin;
                return RedirectToAction("Index", "Admin");
            }

            TempData["MensagemLoginErro"] = "Login ou Senha inválidos";
            return View("Index");
        }
    }
}