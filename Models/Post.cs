using System.ComponentModel.DataAnnotations;

namespace PoliNewsWeb.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int CanalId { get; set; }
        public string? Foto { get; set; }
        public string? Texto { get; set; }
        public string? Titulo { get; set; }
        public string? VideoUrl { get; set; }

        public int Curtidas { get; set; } = 0;

        public Canal? Canal { get; set; }

        // Lista de comentários deste post (fica vazia até alguém comentar)
        public List<Comentario>? Comentarios { get; set; }
    }
}