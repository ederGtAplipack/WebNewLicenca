using LicencaApi.Auth;
using LicencaApi.DTOs;
using LicencaApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Data
{
    //DbContext para a aplicação LicencaApi, representa o contexto do banco de dados
    public class LicencaDbContext : IdentityDbContext<ApplicationUser>
    {
        // Construtor que recebe as opções de configuração do DbContext
        public LicencaDbContext(DbContextOptions<LicencaDbContext> options)
            : base(options) { }

        // DbSets para as entidades do modelo
        public DbSet<LicencaModel> Licenca { get; set; }
        public DbSet<ContratoModel> Contratos { get; set; }
        public DbSet<AcessoNewModel> AcessosNew { get; set; } // Adicionando o DbSet para AcessoNewModel
        public DbSet<SoftwareModel> Software { get; set; }
        //public DbSet<AnagraficaModel> Anagrafica { get; set; }
        public DbSet<LicencasChaveModel> LicencasChave { get; set; }
        public DbSet<RevendaModel> Revenda { get; set; }
        public DbSet<LicencaDetalhadaDTO> LicencaDetalhadaDTOs { get; set; } // Adicionando o DbSet para LicencaDetalhadaDTO
        public DbSet<AnagraficaModel> Anagrafica{ get; set; } // Adicionando o DbSet para AnagraficaDetalhadaDTO

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Garanta que o nome da tabela seja "licencas" (plural e minúsculo, conforme o erro indica 'l.Status')
            //modelBuilder.Entity<LicencaModel>().ToTable("licenca");

            // Garanta a chave primária
            modelBuilder.Entity<LicencaModel>().HasKey(l => l.NumLic); // Ou qual for sua PK

            modelBuilder.Entity<LicencaDetalhadaDTO>().HasKey(l => l.NumLic); // Definindo a chave primária para LicencaDetalhadaDTO

            modelBuilder.Entity<CriarAnagraficaDTO>().HasKey(a => a.IdAnagrafica); // Definindo a chave primária para AnagraficaModel

            modelBuilder.Entity<AnagraficaModel>().HasKey(a => a.IdAnagrafica); // Definindo a chave primária para AnagraficaModel
            // Mapeamento para IdLicencaChave, se não for padrão
            /*modelBuilder.Entity<LicencaModel>().Property(l => l.IdLicencaChave)
                .HasColumnName("idlicencachave")
                .HasColumnType("varchar(255)");*/

            // Ajuste o tamanho conforme necessário
            // Mapeamento para o campo Status
            modelBuilder.Entity<LicencaModel>().Property(l => l.Status)
                .HasColumnName("Status") // O nome da coluna no DB será "Status"
                .HasColumnType("varchar(50)"); // Um tamanho razoável para o status

            // Mapeamentos para as colunas renomeadas no Migrations, se necessário:
            // Se você renomeou "Software" para "software" no DB via migração
            // e quer manter a propriedade "Software" no Model, adicione:
            // modelBuilder.Entity<LicencaModel>().Property(l => l.Software).HasColumnName("software");
            // Repita para todas as colunas que foram renomeadas (Scade, Processador, etc.)
            // ou se o seu modelo já usa os nomes exatos das colunas (snake_case/lowercase).

            base.OnModelCreating(modelBuilder);
        }
    }
}