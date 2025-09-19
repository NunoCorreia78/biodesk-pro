using System.ComponentModel.DataAnnotations;

namespace BioDesk.App.Models;

public class Paciente
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string NomeCompleto { get; set; } = string.Empty;
    
    [Required]
    public DateTime DataNascimento { get; set; }
    
    // Propriedade calculada para idade
    public int Idade => DateTime.Now.Year - DataNascimento.Year - 
        (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);
    
    [MaxLength(20)]
    public string? Genero { get; set; }
    
    [MaxLength(15)]
    public string? NIF { get; set; }
    
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
    
    [MaxLength(100)]
    public string? Profissao { get; set; }
    
    [MaxLength(50)]
    public string? EstadoCivil { get; set; }
    
    [MaxLength(50)]
    public string? LocalHabitual { get; set; }
    
    [MaxLength(100)]
    public string? ComoConheceu { get; set; }
    
    [MaxLength(200)]
    public string? QuemRecomendou { get; set; }
    
    [MaxLength(500)]
    public string? Observacoes { get; set; }
    
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    
    public DateTime? UltimaConsulta { get; set; }
    
    public bool Ativo { get; set; } = true;
}