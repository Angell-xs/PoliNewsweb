using Microsoft.AspNetCore.Mvc;
using PoliNewsWeb.Models;

namespace PoliNewsWeb.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly AppDbContext _contexto;

        public NoticiaController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: Noticia/Criar?categoriaId=2
        public IActionResult Criar(int categoriaId)
        {
            var noticia = new Noticia { CategoriaNoticiaId = categoriaId };
            return View(noticia);
        }
        // GET: Noticia/Ler/5
public IActionResult Ler(int id)
{
    var noticia = _contexto.Noticias.Find(id);
    if (noticia == null) return NotFound();
    return View(noticia);
}

        // POST: Noticia/Criar
        [HttpPost]
        public IActionResult Criar(Noticia novaNoticia)
        {
            _contexto.Noticias.Add(novaNoticia);
            _contexto.SaveChanges();
            return RedirectToAction("Perfil", "CategoriaNoticia", new { id = novaNoticia.CategoriaNoticiaId });
        }

        // GET: Noticia/Editar/5
        public IActionResult Editar(int id)
        {
            var noticia = _contexto.Noticias.Find(id);
            if (noticia == null) return NotFound();
            return View(noticia);
        }

        // POST: Noticia/Editar/5
        [HttpPost]
public IActionResult Editar(int id, Noticia noticiaEditada)
{
    var noticia = _contexto.Noticias.Find(id);
    if (noticia == null) return NotFound();

    noticia.Titulo = noticiaEditada.Titulo;
    noticia.Foto = noticiaEditada.Foto;
    noticia.Texto = noticiaEditada.Texto;
    _contexto.SaveChanges();

    return RedirectToAction("Perfil", "CategoriaNoticia", new { id = noticia.CategoriaNoticiaId });
}

        // GET: Noticia/Excluir/5
        public IActionResult Excluir(int id)
        {
            var noticia = _contexto.Noticias.Find(id);
            if (noticia == null) return NotFound();
            return View(noticia);
        }

        // POST: Noticia/ExcluirConfirmado/5
        [HttpPost]
        public IActionResult ExcluirConfirmado(int id)
        {
            var noticia = _contexto.Noticias.Find(id);
            if (noticia == null) return NotFound();

            int categoriaId = noticia.CategoriaNoticiaId;
            _contexto.Noticias.Remove(noticia);
            _contexto.SaveChanges();

            return RedirectToAction("Perfil", "CategoriaNoticia", new { id = categoriaId });
        }
    }
}