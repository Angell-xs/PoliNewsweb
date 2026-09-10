using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PoliNewsWeb.Models;

namespace PoliNewsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _contexto;

        public HomeController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var canais = _contexto.Canais.ToList();
            var viewModel = new HomeViewModel();

            foreach (var canal in canais)
            {
                var postsDoCanal = _contexto.Posts
                    .Where(p => p.CanalId == canal.Id)
                    .OrderByDescending(p => p.Id)
                    .Take(3)
                    .ToList();

                viewModel.Canais.Add(new CanalComPosts
                {
                    Canal = canal,
                    Posts = postsDoCanal
                });
            }
            viewModel.MateriaDoDia = _contexto.Materias
    .OrderByDescending(m => m.DataPublicacao)
    .FirstOrDefault();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}