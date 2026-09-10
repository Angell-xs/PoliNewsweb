namespace PoliNewsWeb.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Texto { get; set; }
        public string? Foto { get; set; }
        public int Curtidas { get; set; } = 0;
        public int Descurtidas { get; set; } = 0;
        public DateTime DataPublicacao { get; set; } = DateTime.Now;
    }
}