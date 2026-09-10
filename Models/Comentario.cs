using System.ComponentModel.DataAnnotations;

namespace PoliNewsWeb.Models
{
    public class Comentario
    {
        public int Id { get; set; }

        // Liga o comentário a um Post específico
        public int PostId { get; set; }
        public Post? Post { get; set; }   // propriedade de navegação, igual fizemos em Post.Canal

        [Required(ErrorMessage = "Escreve algo antes de enviar!")]
        [StringLength(300)]
        public string Texto { get; set; } = string.Empty;

        public string Autor { get; set; } = "Anônimo";

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}