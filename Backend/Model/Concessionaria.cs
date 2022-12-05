using Ardalis.GuardClauses;

namespace Model;


public class Concessionaria
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public virtual List<Alocacao> Alocacoes { get; set; }


    public Concessionaria(Concessionaria c) : this(c.Nome)
    {
        Id = c.Id;
    }

    private Concessionaria(string nome)
    {
        Guard.Against.NullOrWhiteSpace(nome);

        nome = nome.Trim();

        if (nome.Length < 2)
            throw new ArgumentException("O nome da concessionária deve conter ao menos 2 caracteres.");

        Nome = nome;
    }


    public static async Task<Concessionaria> CreateAsync(string nome)
    {
        var concessionaria = new Concessionaria(nome);

        using var context = new Context();

        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.AddAsync(concessionaria);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return concessionaria;
    }
}
