using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MinimalAPIP;
using MinimalAPIP.Entidades;

var builder = WebApplication.CreateBuilder(args);
//var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos");
//Inicio de area de los servicios
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));
/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));*/

builder.Services.AddCors(opciones =>
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    })
);

builder.Services.AddOutputCache();
builder.Services.AddSwaggerGen();

//Fin de area de los servicios

var app = builder.Build();
//Inicio de area de los Middleware

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();

app.UseOutputCache();

app.MapGet("/", () => "Hello World!");

app.MapGet("/generos", () =>
{
    var generos = new List<Genero>
    {
        new Genero
        {
            Id = 1,
            Nombre = "Drama"
        },
        new Genero
        {
            Id = 2,
            Nombre = "Accion"
        },
        new Genero
        {
            Id = 3,
            Nombre = "Comedia"
        }
    };
    return generos;
}
).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15)));

//Fin de area de los Middlware


app.Run();
