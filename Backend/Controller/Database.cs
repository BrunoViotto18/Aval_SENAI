using Controller.Enums;
using Model.Enums;
using Model;

namespace Controller;


public static class Database
{
    public static async Task InitAsync()
    {
        await InsertAutomoveisAsync();

        await InsertConcessionariasAsync();

        await InsertClientesAsync();

        await InsertAlocacaoAsync();
    }

    private static async Task InsertAutomoveisAsync()
    {
        var automoveis = await Csv.ToDataFrameAsync(SqlData.Automovel.Path);
        for (int i = 0; i < automoveis["id"].Length; i++)
        {
            var modelo = automoveis["modelo"][i];
            var preco = automoveis["preco"][i][4..].Replace(".", "");
            await Automovel.CreateAsync(modelo, float.Parse(preco));
        }
    }

    private static async Task InsertConcessionariasAsync()
    {
        var concessionarias = await Csv.ToDataFrameAsync(SqlData.Concessionaria.Path);
        for (int i = 0; i < concessionarias["id"].Length; i++)
        {
            var nome = concessionarias["concessionaria"][i];
            await Concessionaria.CreateAsync(nome);
        }
    }

    private static async Task InsertClientesAsync()
    {
        var clientes = await Csv.ToDataFrameAsync(SqlData.Cliente.Path);
        for (int i = 0; i < clientes["id"].Length; i++)
        {
            var nome = clientes["nome"][i];
            await Cliente.CreateAsync(nome);
        }
    }

    private static async Task InsertAlocacaoAsync()
    {
        var alocacoes = await Csv.ToDataFrameAsync(SqlData.Alocacao.Path);
        for (int i = 0; i < alocacoes["id"].Length; i++)
        {
            var quantidade = int.Parse(alocacoes["quantidade"][i]);
            var area = int.Parse(alocacoes["area"][i]);
            var automovelId = int.Parse(alocacoes["automovel"][i]);
            var concessinariaId = int.Parse(alocacoes["concessionaria"][i]);

            await Alocacao.CreateAsync(quantidade, (Area)area, automovelId, concessinariaId);
        }
    }
}
