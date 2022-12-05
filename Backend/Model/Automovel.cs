using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Model;


public class Automovel
{
    public int Id { get; set; }
    public string Modelo { get; set; }
    public float Preco { get; set; }

    public virtual List<Alocacao> Alocacoes { get; set; }


    private Automovel() { }
    internal Automovel(Automovel a) : this(a.Modelo, a.Preco)
    {
        Id = a.Id;
    }

    private Automovel(string modelo, float preco)
    {
        Guard.Against.NullOrWhiteSpace(modelo);
        Guard.Against.NegativeOrZero(preco);

        modelo = modelo.Trim();

        if (modelo.Length < 2)
            throw new ArgumentException("O modelo do automovel deve conter ao menos dois caracteres.");

        Modelo = modelo;
        Preco = preco;
    }


    public static async Task<Automovel> CreateAsync(string modelo, float preco)
    {
        var automovel = new Automovel(modelo, preco);

        var context = new Context();

        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.AddAsync(automovel);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return automovel;
    }
}
