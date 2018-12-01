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
    public class CervejaController : BaseController
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

            return View(xml.ListaCervejas);
        }

        public ActionResult Create()
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");
            return View(new Cerveja());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cerveja cerveja, HttpPostedFileBase fotoCerveja)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var xml = Util.CarregarXML();

                if (xml.ListaCervejas == null)
                    xml.ListaCervejas = new List<Cerveja>();

                xml.ListaCervejas.Add(cerveja);

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

        public ActionResult Edit(Guid? id)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var xml = Util.CarregarXML();

            Cerveja cerveja = xml.ListaCervejas.FirstOrDefault(c => c.CervejaId == id);

            if (cerveja == null)
            {
                EscreverMensagem("Ítem não encontrado.", TipoMensagem.Aviso);
                return RedirectToAction("Index");
            }
            return View(cerveja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cerveja cerveja, HttpPostedFileBase fotoCerveja)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var xml = Util.CarregarXML();
                var item = xml.ListaCervejas.FirstOrDefault(c => c.CervejaId == cerveja.CervejaId);
               
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
                item.QuantidadeMl = cerveja.QuantidadeMl;
                item.Temperatura = cerveja.Temperatura;
                item.Posicao = cerveja.Posicao;

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
            var cerveja = xml.ListaCervejas.FirstOrDefault(c => c.CervejaId == id);
          
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

        public ActionResult Preview1(Guid id)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            var xml = Util.CarregarXML();
            var cerveja = xml.ListaCervejas.FirstOrDefault(c => c.CervejaId == id);
            return PartialView("_NossaCervejaPreview1", cerveja);            
        }

        public ActionResult Preview2(Guid id)
        {
            if (String.IsNullOrEmpty(Login.Login))
                return RedirectToAction("Index", "Login");

            var xml = Util.CarregarXML();
            var cerveja = xml.ListaCervejas.FirstOrDefault(c => c.CervejaId == id);
            return PartialView("_NossaCervejaPreview2", cerveja);
        }
    }
}
