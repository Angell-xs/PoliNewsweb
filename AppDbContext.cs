using Microsoft.EntityFrameworkCore;
using PoliNewsWeb.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Canal> Canais { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<CategoriaNoticia> CategoriasNoticia { get; set; }
    public DbSet<Noticia> Noticias { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
}