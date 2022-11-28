using Ardalis.SmartEnum;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Controller.Enums;

public sealed class SqlData : SmartEnum<SqlData>
{
    public static readonly string BasePath = @"..\SqlData";
    public static readonly SqlData Alocacao = new SqlData("Alocação", 0, $@"{BasePath}\alocacao.csv");
    public static readonly SqlData Automovel = new SqlData("Automovel", 1, $@"{BasePath}\automoveis.csv");
    public static readonly SqlData Cliente = new SqlData("Cliente", 2, $@"{BasePath}\clientes.csv");
    public static readonly SqlData Concessionaria = new SqlData("Concessionária", 3, $@"{BasePath}\concessionarias.csv");

    public string Path { get; private set; }

    private SqlData(string name, int value, string path) : base(name, value)
    {
        Path = path;
    }
}
