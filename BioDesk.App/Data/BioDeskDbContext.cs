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
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BioDesk", "pacientes.db");
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
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
            entity.Property(e => e.PrimeiroNome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.UltimoNome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DataNascimento).IsRequired();
            entity.Property(e => e.Telefone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Morada).HasMaxLength(200);
            entity.Property(e => e.Cidade).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Pais).HasMaxLength(50);
            entity.Property(e => e.NumeroUtente).HasMaxLength(20);
            entity.Property(e => e.Observacoes).HasMaxLength(500);
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.Ativo).IsRequired();
            
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.NumeroUtente).IsUnique();
        });
        
        // Dados de exemplo
        modelBuilder.Entity<Paciente>().HasData(
            new Paciente
            {
                Id = 1,
                PrimeiroNome = "João",
                UltimoNome = "Silva",
                DataNascimento = new DateTime(1985, 3, 15),
                Telefone = "912345678",
                Email = "joao.silva@email.com",
                Morada = "Rua das Flores, 123",
                Cidade = "Lisboa",
                CodigoPostal = "1000-001",
                Pais = "Portugal",
                NumeroUtente = "123456789",
                DataCriacao = DateTime.Now.AddDays(-30),
                UltimaConsulta = DateTime.Now.AddDays(-7)
            },
            new Paciente
            {
                Id = 2,
                PrimeiroNome = "Maria",
                UltimoNome = "Santos",
                DataNascimento = new DateTime(1978, 8, 22),
                Telefone = "923456789",
                Email = "maria.santos@email.com",
                Morada = "Avenida Central, 456",
                Cidade = "Porto",
                CodigoPostal = "4000-002",
                Pais = "Portugal",
                NumeroUtente = "987654321",
                DataCriacao = DateTime.Now.AddDays(-45),
                UltimaConsulta = DateTime.Now.AddDays(-14)
            },
            new Paciente
            {
                Id = 3,
                PrimeiroNome = "Carlos",
                UltimoNome = "Ferreira",
                DataNascimento = new DateTime(1992, 12, 5),
                Telefone = "934567890",
                Email = "carlos.ferreira@email.com",
                Morada = "Praceta do Sol, 789",
                Cidade = "Braga",
                CodigoPostal = "4700-003",
                Pais = "Portugal",
                NumeroUtente = "456789123",
                DataCriacao = DateTime.Now.AddDays(-20),
                UltimaConsulta = DateTime.Now.AddDays(-3)
            }
        );
    }
}