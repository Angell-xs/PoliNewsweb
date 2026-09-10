using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliNewsWeb.Models;

namespace PoliNewsWeb.Controllers
{
    public class CategoriaNoticiaController : Controller
    {
        private readonly AppDbContext _contexto;

        public CategoriaNoticiaController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: CategoriaNoticia
        public IActionResult Index()
{
    var categorias = _contexto.CategoriasNoticia.ToList();
    var noticias = _contexto.Noticias
        .Include(n => n.CategoriaNoticia)
        .ToList();

    ViewBag.Noticias = noticias;
    return View(categorias);
}

        // GET: CategoriaNoticia/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: CategoriaNoticia/Criar
        [HttpPost]
        public IActionResult Criar(CategoriaNoticia novaCategoria)
        {
            _contexto.CategoriasNoticia.Add(novaCategoria);
            _contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CategoriaNoticia/Editar/5
        public IActionResult Editar(int id)
        {
            var categoria = _contexto.CategoriasNoticia.Find(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        // POST: CategoriaNoticia/Editar/5
        [HttpPost]
        public IActionResult Editar(int id, CategoriaNoticia categoriaEditada)
        {
            var categoria = _contexto.CategoriasNoticia.Find(id);
            if (categoria == null) return NotFound();

            categoria.Nome = categoriaEditada.Nome;
            _contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CategoriaNoticia/Excluir/5
        public IActionResult Excluir(int id)
        {
            var categoria = _contexto.CategoriasNoticia.Find(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        // POST: CategoriaNoticia/ExcluirConfirmado/5
        [HttpPost]
        public IActionResult ExcluirConfirmado(int id)
        {
            var categoria = _contexto.CategoriasNoticia.Find(id);
            if (categoria == null) return NotFound();

            _contexto.CategoriasNoticia.Remove(categoria);
            _contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CategoriaNoticia/Perfil/5
        public IActionResult Perfil(int id)
        {
            var categoria = _contexto.CategoriasNoticia.Find(id);
            if (categoria == null) return NotFound();

            var noticias = _contexto.Noticias
                .Where(n => n.CategoriaNoticiaId == id)
                .ToList();

            ViewBag.Noticias = noticias;
            return View(categoria);
        }
    }
}