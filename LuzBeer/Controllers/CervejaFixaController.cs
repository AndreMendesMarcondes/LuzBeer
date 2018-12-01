using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuzBeer.Business;
using LuzBeer.Models;

namespace LuzBeer.Controllers
{
    public class CervejaFixaController : BaseController
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


        public ActionResult Create()
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");
            return View(new Cerveja());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CervejaFixa cerveja, HttpPostedFileBase fotoCerveja)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var xml = Util.CarregarXML();

                if (xml.CervejaFixa == null)
                    xml.CervejaFixa = new CervejaFixa();

                xml.CervejaFixa = cerveja;

                if(fotoCerveja != null && fotoCerveja.ContentLength> 0)
                {
                    string caminhoSalvar = Path.Combine(Server.MapPath("~/FotosCervejas"), Path.GetFileName(fotoCerveja.FileName));
                    string caminhoFoto = Path.Combine("/FotosCervejas", Path.GetFileName(fotoCerveja.FileName));
                    cerveja.CaminhoFoto = caminhoFoto;
                    fotoCerveja.SaveAs(caminhoSalvar);
                }

                Util.SalvarXML(xml);
                EscreverMensagem("Ítem criado com sucesso", TipoMensagem.Sucesso);

                return View(cerveja);
            }

            return View(cerveja);
        }

        public ActionResult Index(Guid? id)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            var xml = Util.CarregarXML();

            if (id == null && xml.CervejaFixa == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                CervejaFixa cerveja = xml.CervejaFixa;
               return View("Edit",cerveja);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CervejaFixa cerveja, HttpPostedFileBase fotoCerveja)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var xml = Util.CarregarXML();
                var item = xml.CervejaFixa;
               
                if (fotoCerveja != null && fotoCerveja.ContentLength > 0)
                {
                    string caminhoSalvar = Path.Combine(Server.MapPath("~/FotosCervejas"), Path.GetFileName(fotoCerveja.FileName));
                    string caminhoFoto = Path.Combine("/FotosCervejas", Path.GetFileName(fotoCerveja.FileName));
                    cerveja.CaminhoFoto = caminhoFoto;
                    fotoCerveja.SaveAs(caminhoSalvar);
                    item.CaminhoFoto = cerveja.CaminhoFoto;
                }

                item.Nome = cerveja.Nome;
                item.Amargor = cerveja.Amargor;
                item.Ativo = cerveja.Ativo;              
                item.Coloracao = cerveja.Coloracao;
                item.Descricao = cerveja.Descricao;
                item.DescricaoCompleta = cerveja.DescricaoCompleta;
                item.QuantidadeMl = cerveja.QuantidadeMl;
                item.Temperatura = cerveja.Temperatura;

                Util.SalvarXML(xml);
                EscreverMensagem("Ítem alterado com sucesso", TipoMensagem.Sucesso);
                return View(cerveja);
            }
            return View(cerveja);
        }

        public ActionResult Delete(Guid? id)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            if (id == null)
            {
                EscreverMensagem("Item não encontrado", TipoMensagem.Erro);
                return RedirectToAction("Index");
            }

            var xml = Util.CarregarXML();
            var cerveja = xml.CervejaFixa;
          
            if (cerveja == null)
            {
                EscreverMensagem("Item não encontrado", TipoMensagem.Erro);
                return RedirectToAction("Index");
            }
            return View(cerveja);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            var xml = Util.CarregarXML();
            var cerveja = xml.ListaCervejas.FirstOrDefault(c => c.CervejaId == id);
            xml.ListaCervejas.Remove(cerveja);
            Util.SalvarXML(xml);
            EscreverMensagem("Item excluído com suceso", TipoMensagem.Sucesso);

            return RedirectToAction("Index");
        }
    }
}
