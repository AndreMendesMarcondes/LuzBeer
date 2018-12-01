using System.Web.Mvc;

namespace LuzBeer.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public enum TipoMensagem { Sucesso, Aviso, Erro, Info };

        public void EscreverMensagem(string mensagem, TipoMensagem tipo)
        {
            TempData["MensagemController"] = mensagem;

            if (tipo == TipoMensagem.Aviso)
                TempData["mensagemWarning"] = mensagem;
            else if (tipo == TipoMensagem.Erro)
                TempData["mensagemError"] = mensagem;
            else if (tipo == TipoMensagem.Info)
                TempData["mensagemInfo"] = mensagem;
            else
                TempData["mensagemSuccess"] = mensagem;
        }
    }
}