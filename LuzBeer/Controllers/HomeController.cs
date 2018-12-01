﻿using LuzBeer.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuzBeer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var xml = Util.CarregarXML();
            
            return View(xml);
        }
    }
}