using Ardalis.GuardClauses;

namespace Model;


public class Automovel
{
    public int Id { get; set; }
    public string Modelo { get; set; }
    public float Preco { get; set; }

    public virtual List<Alocacao> Alocacoes { get; set; }


    private Automovel() { }

    private Automovel(string nome, float preco)
    {
        Guard.Against.NullOrWhiteSpace(nome);
        Guard.Against.NegativeOrZero(preco);

        nome = nome.Trim();

        if (nome.Length < 2)
            throw new ArgumentException("O modelo do automovel deve conter ao menos dois caracteres.");

        Modelo = nome;
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
