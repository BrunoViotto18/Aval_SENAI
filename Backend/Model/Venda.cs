namespace Model;


public class Venda
{
    public int Id { get; set; }
    public DateTime Data { get; set; }

    public int AlocacaoId { get; set; }
    public int ClienteId { get; set; }

    public virtual Alocacao Alocacao { get; set; }
    public virtual Cliente Cliente { get; set; }


    private Venda() { }

    public Venda(int clientId, int concessionaraiId)
    {
        // TODO: Verificar se os Ids realmente existem

        ClienteId = clientId;
        AlocacaoId = concessionaraiId;
        Data = DateTime.Now;
    }


    public static async Task<Venda> CreateAsync(int clientId, int alocacaoId)
    {
        var venda = new Venda(clientId, alocacaoId);

        using var context = new Context();

        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var alocacao = await (await Views
                .GetAlocacaoFromIdAsync(alocacaoId))
                .DecreaseQuantityAsync();

            await context.AddAsync(venda);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return venda;
    }
}
