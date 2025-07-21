using Microsoft.EntityFrameworkCore;
using LicencaApi.Models;

namespace LicencaApi.Data
{
    public class LicencaDbContext : DbContext
    {
        public LicencaDbContext(DbContextOptions<LicencaDbContext> options)
            : base(options) { }

        public DbSet<LicencaModel> Licencas { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade LicencaModel
            modelBuilder.Entity<LicencaModel>(entity =>
            {
                // Definindo tabela e chave primária
                entity.ToTable("licenca") // Nome da tabela em minúsculo para consistência
                    .HasKey(l => l.NumLic);

                // Configurando auto-incremento para a chave primária
                entity.Property(l => l.NumLic)
                    .ValueGeneratedOnAdd(); // Adiciona auto-incremento

                // Mapeamento de propriedades
                entity.Property(l => l.Nome_Computador)
                    .HasColumnName("nome_computador");

                entity.Property(l => l.Ip)
                    .HasColumnName("ip");

                entity.Property(l => l.Processador)
                    .HasColumnName("processador");

                entity.Property(l => l.Software)
                    .HasColumnName("software");

                entity.Property(l => l.Tipo_Pc)
                    .HasColumnName("tipo_pc");

                entity.Property(l => l.SistemaOp)
                    .HasColumnName("sistema_op");

                entity.Property(l => l.DataAtivacao)
                    .HasColumnName("data_ativacao");

                entity.Property(l => l.IdLicencaChave)
                    .HasColumnName("id_licenca_chave");

                entity.Property(l => l.IdCliente)
                    .HasColumnName("id_cliente");

                entity.Property(l => l.TipoLic)
                    .HasColumnName("tipo_lic");

                entity.Property(l => l.MacAddress)
                    .HasColumnName("mac_address");

                entity.Property(l => l.DataLic)
                    .HasColumnName("data_lic");

                entity.Property(l => l.Scade)
                    .HasColumnName("scade");

                entity.Property(l => l.Attivo)
                    .HasColumnName("attivo");

                entity.Property(l => l.IdRevenda)
                    .HasColumnName("id_revenda");
            });

            base.OnModelCreating(modelBuilder);
        }*/
    }
}