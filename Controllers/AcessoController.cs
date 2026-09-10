using Microsoft.AspNetCore.Mvc;

namespace PoliNewsWeb.Controllers
{
    public class AcessoController : Controller
    {
        private const string SenhaAluno = "1970";
        private const string SenhaAdmin = "n0LPlAveiTeeed";

        public IActionResult Entrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(string senha)
        {
            if (senha == SenhaAdmin)
            {
                HttpContext.Session.SetString("Autenticado", "sim");
                HttpContext.Session.SetString("TipoUsuario", "admin");
                return RedirectToAction("Index", "Canais");
            }

            if (senha == SenhaAluno)
            {
                HttpContext.Session.SetString("Autenticado", "sim");
                HttpContext.Session.SetString("TipoUsuario", "aluno");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Erro = "Senha incorreta. Tente novamente.";
            return View();
        }
    }
}