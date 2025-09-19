using System.ComponentModel.DataAnnotations;

namespace BioDesk.App.Models;

public class QuestionarioSaude
{
    public int Id { get; set; }
    
    public int PacienteId { get; set; }
    public virtual Paciente Paciente { get; set; } = null!;
    
    // Informações gerais de saúde
    [MaxLength(1000)]
    public string? CondicoesCronicas { get; set; }
    
    [MaxLength(1000)]
    public string? SintomasAtuais { get; set; }
    
    public bool TomaMedicacao { get; set; }
    
    [MaxLength(2000)]
    public string? MedicacaoAtual { get; set; }
    
    // Histórico cirúrgico
    public bool JaFezCirurgias { get; set; }
    
    [MaxLength(2000)]
    public string? HistoricoCirurgias { get; set; }
    
    // Fraturas e lesões
    public bool JaTeveFraturas { get; set; }
    
    [MaxLength(1000)]
    public string? HistoricoFraturas { get; set; }
    
    // Alergias
    public bool TemAlergias { get; set; }
    
    [MaxLength(1000)]
    public string? AlergiasAlimentos { get; set; }
    
    [MaxLength(1000)]
    public string? AlergiasMedicamentos { get; set; }
    
    [MaxLength(1000)]
    public string? AlergiasAmbientais { get; set; }
    
    [MaxLength(1000)]
    public string? AlergiasPlantas { get; set; }
    
    [MaxLength(1000)]
    public string? AlergiasSupplementos { get; set; }
    
    // Histórico familiar
    [MaxLength(2000)]
    public string? HistoricoFamiliar { get; set; }
    
    // Estilo de vida
    public bool Fuma { get; set; }
    public bool ConsomeAlcool { get; set; }
    public bool PraticaExercicio { get; set; }
    
    [MaxLength(500)]
    public string? DetalhesEstiloVida { get; set; }
    
    // Saúde reprodutiva (para mulheres)
    public bool? Gravida { get; set; }
    public bool? Amamentando { get; set; }
    public DateTime? DataUltimaMenstruacao { get; set; }
    
    [MaxLength(500)]
    public string? HistoricoGinecologico { get; set; }
    
    // Sistemas corporais
    public bool ProblemasCardiovasculares { get; set; }
    public bool ProblemasRespiratorios { get; set; }
    public bool ProblemasDigestivos { get; set; }
    public bool ProblemasNeurologicos { get; set; }
    public bool ProblemasEndocrinos { get; set; }
    public bool ProblemasRenais { get; set; }
    public bool ProblemasMusculoesqueleticos { get; set; }
    public bool ProblemasPele { get; set; }
    
    [MaxLength(2000)]
    public string? DetalhesProblemasSaude { get; set; }
    
    // Questões específicas para terapias complementares
    public bool JaFezTerapiasComplementares { get; set; }
    
    [MaxLength(1000)]
    public string? ExperienciaTerapiasComplementares { get; set; }
    
    // Observações e informações adicionais
    [MaxLength(2000)]
    public string? ObservacoesAdicionais { get; set; }
    
    // Controlo
    public DateTime DataPreenchimento { get; set; } = DateTime.Now;
    public DateTime? DataUltimaAtualizacao { get; set; }
    
    public bool Completo { get; set; }
}