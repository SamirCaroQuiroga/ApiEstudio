using System.Data.SqlClient;
using ApiEstudio.Data;
using ApiEstudio.PeliculasMappers;
using ApiEstudio.Repository;
using ApiEstudio.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

//Agregamos los repositorios
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IPeliculaRepositorio, PeliculaRepositorio>();


//Agregamos el AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(PeliculasMapper).Assembly);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

//var logger = app.Logger;

//// Obtener la cadena de conexión
//string connectionString = builder.Configuration.GetConnectionString("ConexionSQL");

//// Probar la conexión usando logger
//try
//{
//    using var connection = new SqlConnection(connectionString);
//    connection.Open();
//    logger.LogInformation("Conexión a base de datos exitosa");
//}
//catch (Exception ex)
//{
//    logger.LogError(ex, "Error al conectar a la base de datos");
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
