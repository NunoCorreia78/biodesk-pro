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
    
    // Sistemas corporais - mantidos para compatibilidade
    public bool ProblemasCardiovasculares { get; set; }
    public bool ProblemasRespiratorios { get; set; }
    public bool ProblemasDigestivos { get; set; }
    public bool ProblemasNeurologicos { get; set; }
    public bool ProblemasEndocrinos { get; set; }
    public bool ProblemasRenais { get; set; }
    public bool ProblemasMusculoesqueleticos { get; set; }
    public bool ProblemasPele { get; set; }
    
    // 1. Sistema Cardiovascular 🫀
    public bool CardioHipertensao { get; set; }
    public bool CardioDiabetesTipo1 { get; set; }
    public bool CardioDiabetesTipo2 { get; set; }
    public bool CardioArritmia { get; set; }
    public bool CardioColesterolAlto { get; set; }
    public bool CardioInsuficienciaCardiaca { get; set; }
    public bool CardioAngina { get; set; }
    public bool CardioOutro { get; set; }
    
    [MaxLength(500)]
    public string? CardioOutroDescricao { get; set; }
    
    // 2. Sistema Musculoesquelético 🦴
    public bool MusculoArtrose { get; set; }
    public bool MusculoTendinite { get; set; }
    public bool MusculoHerniaDiscal { get; set; }
    public bool MusculoFibromialgia { get; set; }
    public bool MusculoArtrite { get; set; }
    public bool MusculoBursite { get; set; }
    public bool MusculoLombalgia { get; set; }
    public bool MusculoCervicalgia { get; set; }
    public bool MusculoOutro { get; set; }
    
    [MaxLength(500)]
    public string? MusculoOutroDescricao { get; set; }
    
    // 3. Sistema Respiratório 🫁
    public bool RespiratorioAsma { get; set; }
    public bool RespiratorioSinusiteCronica { get; set; }
    public bool RespiratorioBronquite { get; set; }
    public bool RespiratorioRiniteAlergica { get; set; }
    public bool RespiratorioApneiaSono { get; set; }
    public bool RespiratorioOutro { get; set; }
    
    [MaxLength(500)]
    public string? RespiratorioOutroDescricao { get; set; }
    
    // 4. Sistema Digestivo 🍃
    public bool DigestivoGastrite { get; set; }
    public bool DigestivoRefluxo { get; set; }
    public bool DigestivoSII { get; set; } // Síndrome Intestino Irritável
    public bool DigestivoIntoleranciaLactose { get; set; }
    public bool DigestivoDoencaCrohn { get; set; }
    public bool DigestivoColiteUlcerosa { get; set; }
    public bool DigestivoConstipacao { get; set; }
    public bool DigestivoOutro { get; set; }
    
    [MaxLength(500)]
    public string? DigestivoOutroDescricao { get; set; }
    
    // 5. Sistema Neurológico 🧠
    public bool NeurologicoEnxaquecas { get; set; }
    public bool NeurologicoAnsiedade { get; set; }
    public bool NeurologicoDepressao { get; set; }
    public bool NeurologicoInsonia { get; set; }
    public bool NeurologicoEpilepsia { get; set; }
    public bool NeurologicoVertigensTonturas { get; set; }
    public bool NeurologicoOutro { get; set; }
    
    [MaxLength(500)]
    public string? NeurologicoOutroDescricao { get; set; }
    
    // 6. Sistema Endócrino 🦋
    public bool EndocrinoHipotiroidismo { get; set; }
    public bool EndocrinoHipertiroidismo { get; set; }
    public bool EndocrinoPCOS { get; set; }
    public bool EndocrinoResistenciaInsulina { get; set; }
    public bool EndocrinoMenopausa { get; set; }
    public bool EndocrinoOutro { get; set; }
    
    [MaxLength(500)]
    public string? EndocrinoOutroDescricao { get; set; }
    
    // 7. Sistema Renal/Geniturinário 🔄
    public bool RenalInfecaoUrinaria { get; set; }
    public bool RenalCalculosRenais { get; set; }
    public bool RenalCistite { get; set; }
    public bool RenalIncontinencia { get; set; }
    public bool RenalOutro { get; set; }
    
    [MaxLength(500)]
    public string? RenalOutroDescricao { get; set; }
    
    // 8. Sistema Pele/Dermatológico 🌟
    public bool PeleEczema { get; set; }
    public bool PelePsoriase { get; set; }
    public bool PeleDermatite { get; set; }
    public bool PeleAlergiasCutaneas { get; set; }
    public bool PeleAcne { get; set; }
    public bool PeleOutro { get; set; }
    
    [MaxLength(500)]
    public string? PeleOutroDescricao { get; set; }
    
    // 9. Sistema Oftalmológico 👁️
    public bool OftalmologicoMiopia { get; set; }
    public bool OftalmologicoHipermetropia { get; set; }
    public bool OftalmologicoAstigmatismo { get; set; }
    public bool OftalmologicoGlaucoma { get; set; }
    public bool OftalmologicoCatarata { get; set; }
    public bool OftalmologicoConjuntivite { get; set; }
    public bool OftalmologicoOutro { get; set; }
    
    [MaxLength(500)]
    public string? OftalmologicoOutroDescricao { get; set; }
    
    // 10. Sistema Auditivo/Otorrino 👂
    public bool AuditivoPerdasAuditivas { get; set; }
    public bool AuditivoZumbidoOuvido { get; set; }
    public bool AuditivoOtitesRecorrentes { get; set; }
    public bool AuditivoLabirintite { get; set; }
    public bool AuditivoSinusite { get; set; }
    public bool AuditivoOutro { get; set; }
    
    [MaxLength(500)]
    public string? AuditivoOutroDescricao { get; set; }
    
    // 11. Sistema Saúde Oral 🦷
    public bool SaudeOralCaries { get; set; }
    public bool SaudeOralGengivite { get; set; }
    public bool SaudeOralPeriodontite { get; set; }
    public bool SaudeOralBruxismo { get; set; }
    public bool SaudeOralSensibilidadeDentaria { get; set; }
    public bool SaudeOralMauHalito { get; set; }
    public bool SaudeOralOutro { get; set; }
    
    [MaxLength(500)]
    public string? SaudeOralOutroDescricao { get; set; }
    
    // 12. Sistema Ginecológico/Reprodutivo adicional ♀️
    // Nota: HistoricoGinecologico já existe como string, adicionando campos específicos
    public bool GinecologicoEndometriose { get; set; }
    public bool GinecologicoSOP { get; set; } // Síndrome Ovários Poliquísticos  
    public bool GinecologicoMiomas { get; set; }
    public bool GinecologicoCiclosIrregulares { get; set; }
    public bool GinecologicoInfecaoUrinaria { get; set; }
    public bool GinecologicoOutro { get; set; }
    
    [MaxLength(500)]
    public string? GinecologicoOutroDescricao { get; set; }
    
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