using Microsoft.EntityFrameworkCore;
using BioDesk.App.Models;
using System.IO;

namespace BioDesk.App.Data;

public class BioDeskDbContext : DbContext
{
    public DbSet<Paciente> Pacientes { get; set; }
    
    public BioDeskDbContext(DbContextOptions<BioDeskDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Usar o diretório do executável como base
            var appDir = AppContext.BaseDirectory;
            var dataDir = Path.Combine(appDir, "Data");
            Directory.CreateDirectory(dataDir);
            var dbPath = Path.Combine(dataDir, "BioDesk_Pacientes.db");
            
            // Usar o sistema de logging da App
            try 
            {
                BioDesk.App.App.DebugLog("=== CONFIGURAÇÃO BD ===");
                BioDesk.App.App.DebugLog($"App Directory: {appDir}");
                BioDesk.App.App.DebugLog($"Data Directory: {dataDir}");
                BioDesk.App.App.DebugLog($"DB Path: {dbPath}");
                BioDesk.App.App.DebugLog($"DB Exists: {File.Exists(dbPath)}");
            }
            catch { /* Ignorar se App não está disponível */ }
            
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NomeCompleto).IsRequired().HasMaxLength(200);
            entity.Property(e => e.DataNascimento).IsRequired();
            entity.Property(e => e.Genero).HasMaxLength(20);
            entity.Property(e => e.NIF).HasMaxLength(15);
            entity.Property(e => e.Telefone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Morada).HasMaxLength(200);
            entity.Property(e => e.Cidade).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Profissao).HasMaxLength(100);
            entity.Property(e => e.EstadoCivil).HasMaxLength(50);
            entity.Property(e => e.LocalHabitual).HasMaxLength(50);
            entity.Property(e => e.ComoConheceu).HasMaxLength(100);
            entity.Property(e => e.QuemRecomendou).HasMaxLength(200);
            entity.Property(e => e.Observacoes).HasMaxLength(500);
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.Ativo).IsRequired();
            
            // Índices únicos apenas para valores não-null
            entity.HasIndex(e => e.Email).IsUnique().HasFilter("Email IS NOT NULL AND Email != ''");
            entity.HasIndex(e => e.NIF).IsUnique().HasFilter("NIF IS NOT NULL AND NIF != ''");
        });
        
        // Dados de exemplo atualizados
        modelBuilder.Entity<Paciente>().HasData(
            new Paciente
            {
                Id = 1,
                NomeCompleto = "João Silva",
                DataNascimento = new DateTime(1985, 3, 15),
                Genero = "Masculino",
                NIF = "123456789",
                Telefone = "912345678",
                Email = "joao.silva@email.com",
                Morada = "Rua das Flores, 123\n1000-001 Lisboa",
                Cidade = "Lisboa",
                CodigoPostal = "1000-001",
                Profissao = "Engenheiro",
                EstadoCivil = "Casado(a)",
                LocalHabitual = "Chão de Lopes",
                ComoConheceu = "Redes Sociais",
                DataCriacao = DateTime.Now.AddDays(-30),
                UltimaConsulta = DateTime.Now.AddDays(-7)
            },
            new Paciente
            {
                Id = 2,
                NomeCompleto = "Maria Santos",
                DataNascimento = new DateTime(1978, 8, 22),
                Genero = "Feminino",
                NIF = "987654321",
                Telefone = "923456789",
                Email = "maria.santos@email.com",
                Morada = "Avenida Central, 456\n4000-002 Porto",
                Cidade = "Porto",
                CodigoPostal = "4000-002",
                Profissao = "Professora",
                EstadoCivil = "Solteiro(a)",
                LocalHabitual = "Samora Correia",
                ComoConheceu = "Recomendação",
                QuemRecomendou = "Ana Costa",
                DataCriacao = DateTime.Now.AddDays(-45),
                UltimaConsulta = DateTime.Now.AddDays(-14)
            },
            new Paciente
            {
                Id = 3,
                NomeCompleto = "Carlos Ferreira",
                DataNascimento = new DateTime(1992, 12, 5),
                Genero = "Masculino",
                NIF = "456789123",
                Telefone = "934567890",
                Email = "carlos.ferreira@email.com",
                Morada = "Praceta do Sol, 789\n4700-003 Braga",
                Cidade = "Braga",
                CodigoPostal = "4700-003",
                Profissao = "Designer",
                EstadoCivil = "União de Facto",
                LocalHabitual = "Online",
                ComoConheceu = "Website/Google",
                DataCriacao = DateTime.Now.AddDays(-20),
                UltimaConsulta = DateTime.Now.AddDays(-3)
            }
        );
    }
}