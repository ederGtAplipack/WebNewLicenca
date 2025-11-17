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
        public DbSet<GenerateMultipleLicensesDTO> GenerateMultipleLicensesDTOs { get; set; } // Adicionando o DbSet para GenerateMultipleLicensesDTO
        public DbSet<AcessoNewModel> AcessosNew { get; set; } // Adicionando o DbSet para AcessoNewModel
        public DbSet<SoftwareModel> Software { get; set; }
        //public DbSet<AnagraficaModel> Anagrafica { get; set; }
        public DbSet<LicencasChaveModel> LicencasChave { get; set; }
        public DbSet<LicencaDispositivoModel> LicencaDispositivo { get; set; }
        public DbSet<LicencaLogModel> LicencaLog { get; set; }
        public DbSet<RevendaModel> Revenda { get; set; }
        public DbSet<RevendaUserModel> RevendaUser { get; set; }
        public DbSet<LicencaDetalhadaDTO> LicencaDetalhadaDTOs { get; set; } // Adicionando o DbSet para LicencaDetalhadaDTO
        public DbSet<ContratoDetalhadoDTO> contratoDetalhadoDTOs { get; set; }
        public DbSet<AnagraficaModel> Anagrafica{ get; set; } // Adicionando o DbSet para AnagraficaDetalhadaDTO
        public DbSet<SoftwareModel> Softwares { get; set; } // DbSet para SoftwareModel
        public DbSet<LicencaDispositivos> LicencaDispositivos { get; set; } // DbSet para LicencaDispositivos
        public DbSet<auditlog> auditlog { get; set; } // DbSet para AuditLog

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Garanta que o nome da tabela seja "licencas" (plural e minúsculo, conforme o erro indica 'l.Status')
            //modelBuilder.Entity<LicencaModel>().ToTable("licenca");

            // Garanta a chave primária
            modelBuilder.Entity<LicencaModel>().HasKey(l => l.NumLic); // Ou qual for sua PK

            modelBuilder.Entity<SoftwareModel>().HasKey(s => s.IdSoftware); // Definindo a chave primária para SoftwareModel

            modelBuilder.Entity<ContratoModel>().HasKey(c => c.IdContrato); // Definindo a chave primária para ContratoModel

            modelBuilder.Entity<LicencaDispositivoModel>().HasKey(ld => ld.idDispositivo); // Definindo a chave primária para LicencaDispositivoModel

            modelBuilder.Entity<LicencaLogModel>().HasKey(ll => ll.idLog); // Definindo a chave primária para LicencaLogModel

            modelBuilder.Entity<LicencasChaveModel>().HasKey(lc => lc.IdLicencaChave); // Definindo a chave primária para LicencasChaveModel

            modelBuilder.Entity<GenerateMultipleLicensesDTO>(entity =>
            {
                entity.HasNoKey(); // Indica que esta entidade não tem chave primária
                entity.ToView(null); // Opcional: define o nome da tabela, se necessário
            });

            modelBuilder.Entity<RevendaModel>().HasKey(r => r.idRevenda); // Definindo a chave primária para RevendaModel

            modelBuilder.Entity<LicencaDetalhadaDTO>().HasKey(l => l.NumLic); // Definindo a chave primária para LicencaDetalhadaDTO

            modelBuilder.Entity<ContratoDetalhadoDTO>().HasKey(ctr => ctr.idContrato);

            modelBuilder.Entity<CriarAnagraficaDTO>().HasKey(a => a.IdAnagrafica); // Definindo a chave primária para AnagraficaModel

            //modelBuilder.Entity<CriarContratoDTO>().HasKey(ctr => ctr.idContrato);

            modelBuilder.Entity<CriarRevendaDTO>().HasKey(crv => crv.idRevenda);

            modelBuilder.Entity<RevendaUserModel>().ToTable("revenda_user");

            modelBuilder.Entity<LicencaDeviceDTO>().HasKey(ld => ld.IdDispositivo);

            modelBuilder.Entity<auditlog>().HasKey(al => al.IdLog); // Definindo a chave primária para AuditLog

            // Configura a chave primária composta
            modelBuilder.Entity<RevendaUserModel>()
                .HasKey(ru => new { ru.idRevenda, ru.idUser });

            // Configura a chave estrangeira para RevendaModel
            modelBuilder.Entity<RevendaUserModel>()
                .HasOne(ru => ru.Revenda) // Mapeia para a propriedade de navegação
                .WithMany() // Revenda pode ter muitos RevendaUser
                .HasForeignKey(ru => ru.idRevenda); // Usa a propriedade idRevenda como FK

            // Configura a chave estrangeira para o usuário.
            // Assumindo que você tem uma classe de usuário, como 'ApplicationUser'.
            modelBuilder.Entity<RevendaUserModel>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(ru => ru.idUser);

            modelBuilder.Entity<ContratoModel>()
                .HasMany(c => c.Licencas) // Propriedade de navegação para Licencas
                .WithOne(l => l.Contrato) // Propriedade de navegação inversa em LicencaModel
                .HasForeignKey(l => l.IdContrato); // Chave estrangeira em LicencaModel

            modelBuilder.Entity<RevendaUserModel>().HasKey(rv => rv.idRevenda);

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