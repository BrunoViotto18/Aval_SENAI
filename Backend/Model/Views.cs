using Microsoft.EntityFrameworkCore;

namespace Model;

using Enums;
using System.Runtime.CompilerServices;

public static class Views
{
    public static async Task<Area[]> GetAllAllocatedAreasAsync()
    {
        using var context = new Context();

        var allocatedAreas = await context.Alocacao
            .Where(a => a.Quantidade > 0)
            .GroupBy(a => a.Area)
            .Select(g => g.Key)
            .ToArrayAsync();

        return allocatedAreas;
    }

    public static async Task<Automovel[]> GetAllAutomobilesInAreaAsync(Area area)
    {
        using var context = new Context();

        var automobilesAllocatedInArea = await context.Automovel
            .IncludeAlocacoes(context, join => join.Right.Quantidade > 0 && join.Right.Area == area)
            .ToArrayAsync();

        return automobilesAllocatedInArea;
    }

    public static async Task<string> GetAutomobileModelFromIdAsync(int id)
    {
        using var context = new Context();

        var name = (await context.Automovel
            .FirstAsync(auto => auto.Id == id))
            .Modelo;

        return name;
    }

    public static async Task<Cliente[]> GetAllClientesAsync()
    {
        using var context = new Context();

        var clientes = await context.Cliente
            .ToArrayAsync();

        return clientes;
    }

    public static async Task<Concessionaria[]> GetConcessionariasWithModelAllocatedInAreaAsync(string modelo, Area area)
    {
        using var context = new Context();

        var concessionarias = await context.Concessionaria
            .Join(context.Alocacao, conc => conc.Id, aloc => aloc.ConcessionariaId, (conc, aloc) => new
            {
                Concessionaria = conc,
                Alocacao = aloc
            })
            .Where(join => join.Alocacao.Area == area)
            .Join(context.Automovel, join => join.Alocacao.AutomovelId, auto => auto.Id, (join, auto) => new
            {
                Concessionaria = join.Concessionaria,
                Alocacao = join.Alocacao,
                Automovel = auto
            })
            .Where(join => join.Automovel.Modelo == modelo)
            .GroupBy(j => j.Concessionaria.Id)
            .Select(g => new Concessionaria(g.First().Concessionaria))
            .ToArrayAsync();

        return concessionarias;
    }

    public static async Task<Alocacao> GetAlocacaoFromIdAsync(int alocacaoId)
    {
        using var context = new Context();

        var alocacao = await context.Alocacao
            .FirstAsync(a => a.Id == alocacaoId);

        return alocacao;
    }

    public static async Task<Alocacao> GetAlocacaoFromForeignKeysAsync(int automovelId, int concessionariaId)
    {
        using var context = new Context();

        var alocacao = await context.Alocacao
            .FirstAsync(aloc => aloc.ConcessionariaId == concessionariaId && aloc.AutomovelId == automovelId);

        return alocacao;
    }
}
