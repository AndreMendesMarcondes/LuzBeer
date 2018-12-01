using LuzBeer.Business;
using LuzBeer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuzBeer.Controllers
{
    public class KitController : BaseController
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

            var xml = Util.CarregarXML();

            var kit = xml.Kit == null ? new Kit() : xml.Kit;

            return View(kit);
        }

        public ActionResult Salvar(Kit kit, HttpPostedFileBase fotoCerveja)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var xml = Util.CarregarXML();
                var kitXml = xml.Kit == null ? new Kit() : xml.Kit;

                if (fotoCerveja != null && fotoCerveja.ContentLength > 0)
                {
                    string caminhoSalvar = Path.Combine(Server.MapPath("~/FotosCervejas"), Path.GetFileName(fotoCerveja.FileName));
                    string caminhoFoto = Path.Combine("/FotosCervejas", Path.GetFileName(fotoCerveja.FileName));
                    kitXml.CaminhoFotoKit = caminhoFoto;
                    fotoCerveja.SaveAs(caminhoSalvar);
                }

                kitXml.PrimeiraCerveja = kit.PrimeiraCerveja;
                kitXml.PrimeiraDescricao = kit.PrimeiraDescricao;
                kitXml.SegundaCerveja = kit.SegundaCerveja;
                kitXml.SegundaDescricao = kit.SegundaDescricao;
                kitXml.Mes = kit.Mes;
                xml.Kit = kitXml;

                Util.SalvarXML(xml);
                EscreverMensagem("Ítem criado com sucesso", TipoMensagem.Sucesso);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}