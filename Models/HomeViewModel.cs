namespace PoliNewsWeb.Models
{
    public class HomeViewModel
    {
        public Materia? MateriaDoDia { get; set; }
        public List<CanalComPosts> Canais { get; set; } = new();
    }

    public class CanalComPosts
    {
        public Canal Canal { get; set; } = null!;
        public List<Post> Posts { get; set; } = new();
    }
}