namespace Model;


public class Venda
{
    public int Id { get; set; }
    public DateTime Data { get; set; }

    public int AlocacaoId { get; set; }
    public int ClienteId { get; set; }

    public virtual Alocacao Alocacao { get; set; }
    public virtual Cliente Cliente { get; set; }


    private Venda()
    {

    }


}
