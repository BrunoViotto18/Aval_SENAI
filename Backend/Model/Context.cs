using Microsoft.EntityFrameworkCore;

namespace Model;


public class Context : DbContext
{
    public DbSet<Alocacao> Alocacao { get; set; }
    public DbSet<Automovel> Automovel { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Concessionaria> Concessionaria { get; set; }
    public DbSet<Venda> Venda { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer($"Data Source={Environment.MachineName}\\SQLEXPRESS;Initial Catalog=Avaliacao_SENAI;Integrated Security=SSPI;TrustServerCertificate=True");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Alocacao>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Quantidade);
            entity.Property(a => a.Area);

            entity.HasOne(al => al.Automovel)
                .WithMany()
                .HasForeignKey(a => a.AutomovelId);
            entity.HasOne(al => al.Concessionaria)
                .WithMany()
                .HasForeignKey(a => a.ConcessionariaId);
        });

        modelBuilder.Entity<Automovel>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Modelo);
            entity.Property(a => a.Preco);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome);
        });

        modelBuilder.Entity<Concessionaria>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome);
        });

        modelBuilder.Entity<Venda>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Data);

            entity.HasOne(v => v.Alocacao)
                .WithMany(a => a.Vendas)
                .HasForeignKey(v => v.AlocacaoId);
            entity.HasOne(v => v.Cliente)
                .WithMany(c => c.Vendas)
                .HasForeignKey(v => v.ClienteId);
        });
    }
}
