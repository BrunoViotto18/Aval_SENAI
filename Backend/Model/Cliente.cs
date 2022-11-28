using Ardalis.GuardClauses;

namespace Model;


public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public virtual List<Venda> Vendas { get; set; }


    private Cliente(string nome)
    {
        Guard.Against.NullOrWhiteSpace(nome);

        nome = nome.Trim();

        if (nome.Length < 2)
            throw new ArgumentException("O nome do cliente deve conter ao menos dois caracteres.");

        Nome = nome;
    }


    public static async Task<Cliente> CreateAsync(string nome)
    {
        var cliente = new Cliente(nome);

        using var context = new Context();

        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.AddAsync(cliente);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return cliente;
    }
}
