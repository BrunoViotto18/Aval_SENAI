using Ardalis.GuardClauses;
using Model.Enums;

namespace Model;


public class Alocacao
{
    public int Id { get; set; }
    public int Quantidade { get; set; }
    public Area Area { get; set; }

    public int AutomovelId { get; set; }
    public int ConcessionariaId { get; set; }

    public virtual Automovel Automovel { get; set; }
    public virtual Concessionaria Concessionaria { get; set; }

    public virtual List<Venda> Vendas { get; set; }


    private Alocacao(int quantidade, Area area, int automovelId, int concessionariaId)
    {
        Guard.Against.Negative(quantidade);
        Guard.Against.NegativeOrZero(automovelId);
        Guard.Against.NegativeOrZero(concessionariaId);

        // TODO: Checar se os Ids existem e são válidos

        Quantidade = quantidade;
        Area = area;
        AutomovelId = automovelId;
        ConcessionariaId = concessionariaId;
    }


    public static async Task<Alocacao> CreateAsync(int quantidade, Area area, int automovelId, int concessionariaId)
    {
        var alocacao = new Alocacao(quantidade, area, automovelId, concessionariaId);

        using var context = new Context();

        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.AddAsync(alocacao);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return alocacao;
    }
}
