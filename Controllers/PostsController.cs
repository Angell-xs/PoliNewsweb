using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliNewsWeb.Models;

namespace PoliNewsWeb.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext _contexto;

        public PostsController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: Posts/Criar?canalId=2  (post de IMAGEM, usado a partir do Perfil do canal)
        public IActionResult Criar(int canalId)
        {
            var post = new Post { CanalId = canalId };
            return View(post);
        }

        [HttpPost]
        public IActionResult Criar(Post novoPost)
        {
            _contexto.Posts.Add(novoPost);
            _contexto.SaveChanges();

            return RedirectToAction("Perfil", "Canais", new { id = novoPost.CanalId });
        }

        // GET: Posts/CriarVideo  (post de VÍDEO, usado a partir do Feed estilo TikTok)
        public IActionResult CriarVideo()
        {
            ViewBag.Canais = _contexto.Canais.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CriarVideo(Post novoPost)
        {
            _contexto.Posts.Add(novoPost);
            _contexto.SaveChanges();

            return RedirectToAction("Feed");
        }

        // GET: Posts/Excluir/5
        public IActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _contexto.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Excluir")]
        public IActionResult ExcluirConfirmado(int id)
        {
            var post = _contexto.Posts.Find(id);
            int canalId = post.CanalId;

            _contexto.Posts.Remove(post);
            _contexto.SaveChanges();

            return RedirectToAction("Perfil", "Canais", new { id = canalId });
        }

        public IActionResult Feed()
        {
            var posts = _contexto.Posts
                .Include(p => p.Canal)
                .Include(p => p.Comentarios)
                .Where(p => p.VideoUrl != null && p.VideoUrl != "")
                .ToList();

            return View(posts);
        }

        public IActionResult Ver(int id)
        {
            var post = _contexto.Posts
                .Include(p => p.Canal)
                .Include(p => p.Comentarios)
                .FirstOrDefault(p => p.Id == id);

            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        public IActionResult Curtir(int id)
        {
            var post = _contexto.Posts.Find(id);
            if (post == null) return NotFound();

            post.Curtidas++;
            _contexto.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // Salva um novo comentário e volta pra mesma tela
        [HttpPost]
        public IActionResult Comentar(int postId, string texto)
        {
            if (!string.IsNullOrWhiteSpace(texto))
            {
                var comentario = new Comentario
                {
                    PostId = postId,
                    Texto = texto.Trim(),
                    Autor = "Anônimo",
                    DataCriacao = DateTime.Now
                };

                _contexto.Comentarios.Add(comentario);
                _contexto.SaveChanges();
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Posts/Editar/5  (só edita título, texto e foto — não mexe em vídeo)
        public IActionResult Editar(int id)
        {
            var post = _contexto.Posts.Find(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        public IActionResult Editar(Post postEditado)
        {
            var post = _contexto.Posts.Find(postEditado.Id);
            if (post == null) return NotFound();

            post.Titulo = postEditado.Titulo;
            post.Texto = postEditado.Texto;
            post.Foto = postEditado.Foto;

            _contexto.SaveChanges();

            return RedirectToAction("Perfil", "Canais", new { id = post.CanalId });
        }

        // Exclui um comentário (só admin) e devolve a lista atualizada
        [HttpPost]
        public IActionResult ExcluirComentario(int id)
        {
            if (HttpContext.Session.GetString("TipoUsuario") != "admin")
            {
                return Forbid();
            }

            var comentario = _contexto.Comentarios.Find(id);
            if (comentario == null) return NotFound();

            int postId = comentario.PostId;
            _contexto.Comentarios.Remove(comentario);
            _contexto.SaveChanges();

            var comentarios = _contexto.Comentarios
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.DataCriacao)
                .ToList();

            return PartialView("_ComentariosParcial", comentarios);
        }

        // Devolve a listinha de comentários de um post (usado pela caixinha do feed)
        [HttpGet]
        public IActionResult ListarComentarios(int postId)
        {
            var comentarios = _contexto.Comentarios
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.DataCriacao)
                .ToList();

            return PartialView("_ComentariosParcial", comentarios);
        }
    }
}