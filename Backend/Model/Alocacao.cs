using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
using System.Runtime.CompilerServices;

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

    private Alocacao(Alocacao a) : this(a.Quantidade, a.Area, a.AutomovelId, a.ConcessionariaId) { }


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

    public async Task<Alocacao> DecreaseQuantityAsync(int amount=1)
    {
        Guard.Against.NegativeOrZero(amount);
        Guard.Against.Negative(Quantidade - amount);

        Quantidade -= amount;

        using var context = new Context();

        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.Update(this);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return this;
    }
}
