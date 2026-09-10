using Microsoft.EntityFrameworkCore;
using PoliNewsWeb.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=polinews.db"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();  

var app = builder.Build();

// Roda as migrations automaticamente ao ligar o site
// (isso cria as tabelas no polinews.db se elas ainda não existirem —
// necessário porque o banco não vem "pronto" quando o Docker sobe no Render)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    // Se não tiver nenhum canal ainda (banco vazio), cria os 3 canais oficiais
    if (!db.Canais.Any())
    {
        db.Canais.AddRange(
            new Canal
            {
                Nome = "Espia Poli",
                Descricao = "Focado em bastidores, curiosidades, projetos e no melhor da rotina do dia a dia da escola. 👀📚",
                Foto = "https://i.postimg.cc/Pqkn6325/1787502456502.jpg"
            },
            new Canal
            {
                Nome = "Andando por aí",
                Descricao = "Destinado à cobertura de passeios, excursões, eventos externos e viagens da galera fora dos muros escolares. 🌍🎒",
                Foto = "https://i.postimg.cc/x8ZWLtXK/1787502456517.jpg"
            },
            new Canal
            {
                Nome = "Grêmio Estudantil Machado de Assis",
                Descricao = "O canal oficial da voz dos alunos, trazendo avisos, eventos, assembleias e campanhas da chapa. 🏛️✊",
                Foto = "https://i.postimg.cc/QdCJ2hnB/1787502919273.jpg"
            }
        );
        db.SaveChanges();
    }
}

// No Render, a porta certa vem da variável de ambiente PORT.
// Localmente (sua máquina) ela não existe, então usamos 5116 como padrão.
var porta = Environment.GetEnvironmentVariable("PORT") ?? "5116";
app.Urls.Add($"http://0.0.0.0:{porta}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession(); 

// Obriga login: quem não tiver "Autenticado" = "sim" na sessão
// é redirecionado pra tela de senha antes de ver qualquer página.
app.Use(async (context, next) =>
{
    var caminho = context.Request.Path.Value ?? "";
    bool ehPaginaDeLogin = caminho.StartsWith("/Acesso");
    bool ehArquivoEstatico = caminho.StartsWith("/css") || caminho.StartsWith("/js")
        || caminho.StartsWith("/lib") || caminho.StartsWith("/favicon");

    bool autenticado = context.Session.GetString("Autenticado") == "sim";

    if (!autenticado && !ehPaginaDeLogin && !ehArquivoEstatico)
    {
        context.Response.Redirect("/Acesso/Entrar");
        return;
    }

    await next();
});

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
