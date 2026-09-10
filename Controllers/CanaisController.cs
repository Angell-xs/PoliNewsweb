using Microsoft.AspNetCore.Mvc;
using PoliNewsWeb.Models;
using System.Linq;

public class CanaisController : Controller
{
    private readonly AppDbContext _contexto;

    public CanaisController(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public IActionResult Index()
    {
        var canais = _contexto.Canais.ToList();
        return View(canais);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Canal novoCanal)
    {
        _contexto.Canais.Add(novoCanal);
        _contexto.SaveChanges();

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canal = await _contexto.Canais.FindAsync(id);
            if (canal == null)
            {
                return NotFound();
            }
            return View(canal);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Canal canalEditado)
        {
            if (id != canalEditado.Id)
            {
                return NotFound();
            }

            _contexto.Canais.Update(canalEditado);
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Excluir(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var canal = await _contexto.Canais.FindAsync(id);
        if (canal == null)
        {
            return NotFound();
        }
        return View(canal);
    }
    [HttpPost, ActionName("Excluir")]
    public async Task<IActionResult> ExcluirConfirmado(int id)
    {
        var canal = await _contexto.Canais.FindAsync(id);
        if (canal != null)
        {
            _contexto.Canais.Remove(canal);
            await _contexto.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
    // GET: Canais/Perfil/5
    public async Task<IActionResult> Perfil(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var canal = await _contexto.Canais.FindAsync(id);
    if (canal == null)
    {
        return NotFound();
    }

    ViewBag.Posts = _contexto.Posts.Where(p => p.CanalId == id).ToList();

    return View(canal);
}
}