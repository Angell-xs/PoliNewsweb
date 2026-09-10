using Microsoft.AspNetCore.Mvc;
using PoliNewsWeb.Models;

namespace PoliNewsWeb.Controllers
{
    public class MateriaController : Controller
    {
        private readonly AppDbContext _contexto;

        public MateriaController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpPost]
        public IActionResult Curtir(int id)
        {
            var materia = _contexto.Materias.Find(id);
            if (materia == null) return NotFound();

            materia.Curtidas++;
            _contexto.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Descurtir(int id)
        {
            var materia = _contexto.Materias.Find(id);
            if (materia == null) return NotFound();

            materia.Descurtidas++;
            _contexto.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        // GET: Materia/Excluir/5
public IActionResult Excluir(int id)
{
    var materia = _contexto.Materias.Find(id);
    if (materia == null) return NotFound();
    return View(materia);
}

// POST: Materia/ExcluirConfirmado/5
[HttpPost]
public IActionResult ExcluirConfirmado(int id)
{
    var materia = _contexto.Materias.Find(id);
    if (materia == null) return NotFound();

    _contexto.Materias.Remove(materia);
    _contexto.SaveChanges();
    return RedirectToAction("Index", "Home");
}
        // GET: Materia/Criar
public IActionResult Criar()
{
    return View();
}

// POST: Materia/Criar
[HttpPost]
public IActionResult Criar(Materia novaMateria)
{
    _contexto.Materias.Add(novaMateria);
    _contexto.SaveChanges();
    return RedirectToAction("Index", "Home");
}
    }
}