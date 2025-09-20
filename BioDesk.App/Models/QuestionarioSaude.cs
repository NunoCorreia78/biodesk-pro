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
    
    // Sistemas corporais - CAMPOS ANTIGOS (manter por compatibilidade)
    public bool ProblemasCardiovasculares { get; set; }
    public bool ProblemasRespiratorios { get; set; }
    public bool ProblemasDigestivos { get; set; }
    public bool ProblemasNeurologicos { get; set; }
    public bool ProblemasEndocrinos { get; set; }
    public bool ProblemasRenais { get; set; }
    public bool ProblemasMusculoesqueleticos { get; set; }
    public bool ProblemasPele { get; set; }

    // NOVOS CAMPOS ESPECÍFICOS POR SISTEMA - Problemas Cardiovasculares
    public bool CardioHipertensao { get; set; }
    public bool CardioArritmia { get; set; }
    public bool CardioColesterolAlto { get; set; }
    public bool CardioInsuficienciaCardiaca { get; set; }
    public bool CardioAngina { get; set; }
    public bool CardioInfartoMiocardio { get; set; }

    // Problemas Musculoesqueléticos
    public bool MusculoArtrose { get; set; }
    public bool MusculoTendinite { get; set; }
    public bool MusculoHerniaDiscal { get; set; }
    public bool MusculoFibromialgia { get; set; }
    public bool MusculoArtrite { get; set; }
    public bool MusculoBursite { get; set; }
    public bool MusculoLombalgias { get; set; }

    // Problemas Respiratórios
    public bool RespiratorioAsma { get; set; }
    public bool RespiratorioSinusiteCronica { get; set; }
    public bool RespiratorioBronquite { get; set; }
    public bool RespiratorioRiniteAlergica { get; set; }
    public bool RespiratorioApneiaDoSono { get; set; }

    // Problemas Digestivos
    public bool DigestivoGastrite { get; set; }
    public bool DigestivoSindromeIntestinoIrritavel { get; set; }
    public bool DigestivoRefluxoGastroesofagico { get; set; }
    public bool DigestivoIntoleranciaLactose { get; set; }
    public bool DigestivoDoencaCrohn { get; set; }
    public bool DigestivoUlceraPeptica { get; set; }

    // Problemas Neurológicos
    public bool NeurologicoEnxaquecas { get; set; }
    public bool NeurologicoAnsiedade { get; set; }
    public bool NeurologicoDepressao { get; set; }
    public bool NeurologicoInsonia { get; set; }
    public bool NeurologicoEpilepsia { get; set; }
    public bool NeurologicoTranstornoBipolar { get; set; }

    // Problemas Endócrinos
    public bool EndocrinoDiabetesTipo1 { get; set; }
    public bool EndocrinoDiabetesTipo2 { get; set; }
    public bool EndocrinoHipotiroidismo { get; set; }
    public bool EndocrinoHipertiroidismo { get; set; }
    public bool EndocrinoPCOS { get; set; }
    public bool EndocrinoResistenciaInsulina { get; set; }

    // Problemas Renais/Geniturinários
    public bool RenalInfecaoUrinaria { get; set; }
    public bool RenalCalculosRenais { get; set; }
    public bool RenalCistite { get; set; }
    public bool RenalIncontinencia { get; set; }
    public bool RenalInsuficienciaRenal { get; set; }
    
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