using LuzBeer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuzBeer.Controllers
{
    public class AdminController : BaseController
    {
        public Admin Login
        {
            get
            {
                if (TempData["LoginUsuario"] == null)
                    TempData["LoginUsuario"] = new Admin();

                TempData.Keep("LoginUsuario");

                return (Admin)TempData["LoginUsuario"];
            }
            set
            {
                TempData["LoginUsuario"] = value;
            }
        }

        public ActionResult Index()
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            return View();
        }
    }
}