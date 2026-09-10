namespace PoliNewsWeb.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Foto { get; set; }
        public string? Texto { get; set; }
        public string? LinkExterno { get; set; }
        public int CategoriaNoticiaId { get; set; }
        public CategoriaNoticia? CategoriaNoticia { get; set; }
    }
}