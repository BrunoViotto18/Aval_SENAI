using System.Globalization;
using Controller;
using Model;


/* Settando a cultura padrão para pt-BR */

string cultureString = "pt-BR";
CultureInfo ci = new CultureInfo(cultureString);
Thread.CurrentThread.CurrentCulture = ci;
Thread.CurrentThread.CurrentUICulture = ci;


/* Criando o Banco de Dados */

using var context = new Context();

await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();


/* SQL Script */

await Database.InitAsync();


/* Criando a Api */

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
