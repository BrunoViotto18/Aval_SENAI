using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Model;


public class Join<TLeft, TRight>
    where TLeft : class
    where TRight : class
{
    public TLeft Left { get; set; }
    public TRight Right { get; set; }
}


public static class JoinExtensions
{
    public static IQueryable<Automovel> IncludeAlocacoes
        (this DbSet<Automovel> automoveis, Context context, Expression<Func<Join<Automovel, Alocacao>, bool>> filter)
    {
        var included = automoveis
            .Join(context.Alocacao, au => au.Id, al => al.AutomovelId, (au, al) => new Join<Automovel, Alocacao>
            {
                Left = au,
                Right = al
            })
            .Where(filter)
            .GroupBy(g => g.Left.Id)
            .Select(g => new Automovel(g.First().Left)
            {
                Alocacoes = g.Select(grupo => grupo.Right).ToList()
            });

        return included;
    }

    public static IQueryable<Join<Concessionaria, Alocacao>> JoinAlocacoes
        (this DbSet<Concessionaria> concessionarias, Context context, Expression<Func<Join<Concessionaria, Alocacao>, bool>> filter)
    {
        var joined = concessionarias
            .Join(context.Alocacao, conc => conc.Id, aloc => aloc.ConcessionariaId, (auto, aloc) => new Join<Concessionaria, Alocacao>
            {
                Left = auto,
                Right = aloc
            })
            .Where(filter);
            //.GroupBy(g => g.Left.Id)
            //.Select(g => new Concessionaria(g.First().Left)
            //{
            //    Alocacoes = g.Select(grupo => grupo.Right).ToList()
            //});

        return joined;
    }

    public static IQueryable<Join<Join<Concessionaria, Alocacao>, Automovel>> ThenJoinAutomoveis
        (this IQueryable<Join<Concessionaria, Alocacao>> concessionariaAlocacao, Context context, Expression<Func<Join<Join<Concessionaria, Alocacao>, Automovel>, bool>> filter)
    {
        var joined = concessionariaAlocacao
            .Join(context.Automovel, join => join.Right.AutomovelId, auto => auto.Id, (join, auto) => new Join<Join<Concessionaria, Alocacao>, Automovel>
            {
                Left = join,
                Right = auto
            })
            .Where(filter);
        
        return joined;
    }
}
