using System.Globalization;
using System.Text;
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


// CORS
var mySpecificOrigins = "_MySpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(mySpecificOrigins, policy =>
    {
        policy
            .WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/* Criando o App */

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(mySpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
