using System.ComponentModel.DataAnnotations;

namespace BioDesk.App.Models;

public class Paciente
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string PrimeiroNome { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string UltimoNome { get; set; } = string.Empty;
    
    public string NomeCompleto => $"{PrimeiroNome} {UltimoNome}";
    
    [Required]
    public DateTime DataNascimento { get; set; }
    
    [MaxLength(20)]
    public string? Telefone { get; set; }
    
    [MaxLength(150)]
    [EmailAddress]
    public string? Email { get; set; }
    
    [MaxLength(200)]
    public string? Morada { get; set; }
    
    [MaxLength(100)]
    public string? Cidade { get; set; }
    
    [MaxLength(10)]
    public string? CodigoPostal { get; set; }
    
    [MaxLength(50)]
    public string? Pais { get; set; }
    
    [MaxLength(20)]
    public string? NumeroUtente { get; set; }
    
    [MaxLength(500)]
    public string? Observacoes { get; set; }
    
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    
    public DateTime? UltimaConsulta { get; set; }
    
    public bool Ativo { get; set; } = true;
}