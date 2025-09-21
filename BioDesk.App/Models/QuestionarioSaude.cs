using System.ComponentModel.DataAnnotations;

namespace BioDesk.App.Models;

public class QuestionarioSaude
{
    public int Id { get; set; }
    
    public int PacienteId { get; set; }
    public virtual Paciente Paciente { get; set; } = null!;
    
    // ===== 1. DADOS DO UTENTE (automático) =====
    // Preenchidos automaticamente pelos dados pessoais
    
    // ===== 2. QUEIXA PRINCIPAL E OBJETIVOS =====
    [MaxLength(2000)]
    public string? QueixaPrincipal { get; set; }
    
    public bool ObjetivoAlivioDor { get; set; }
    public bool ObjetivoMelhorarMobilidade { get; set; }
    public bool ObjetivoMelhorarDigestao { get; set; }
    public bool ObjetivoReduzirStress { get; set; }
    public bool ObjetivoMelhorarSono { get; set; }
    public bool ObjetivoGestaoWeight { get; set; }
    public bool ObjetivoAumentarVitalidade { get; set; }
    public bool ObjetivoBemEstarGeral { get; set; }
    
    [MaxLength(500)]
    public string? ObjetivoOutro { get; set; }
    
    // ===== 3. ESTADO GERAL =====
    [MaxLength(50)]
    public string? SaudeGlobal { get; set; } // Muito boa | Boa | Regular | Fraca
    
    public int? NivelDorAtual { get; set; } // 0-10
    
    [MaxLength(50)]
    public string? GravidezAleitamento { get; set; } // Não | Grávida | A amamentar
    
    public bool ImplantePacemaker { get; set; }
    public bool ImplanteStent { get; set; }
    public bool ImplanteProtesesArticulares { get; set; }
    public bool ImplantePlacasParafusos { get; set; }
    public bool ImplanteDIU { get; set; }
    
    [MaxLength(500)]
    public string? ImplantesOutros { get; set; }
    
    // ===== 4. INFORMAÇÕES MÉDICAS GERAIS =====
    [MaxLength(2000)]
    public string? DiagnosticosMedicosAtuais { get; set; }
    
    // Doenças crónicas (checkboxes)
    public bool DoencaHipertensao { get; set; }
    public bool DoencaCardiopatia { get; set; }
    public bool DoencaDiabetesTipo1 { get; set; }
    public bool DoencaDiabetesTipo2 { get; set; }
    public bool DoencaTiroideia { get; set; }
    public bool DoencaDislipidemia { get; set; }
    public bool DoencaRenal { get; set; }
    public bool DoencaHepatica { get; set; }
    public bool DoencaAutoimune { get; set; }
    public bool DoencaOsteoporose { get; set; }
    public bool DoencaAsmaDPOC { get; set; }
    public bool DoencaOncologia { get; set; }
    public bool DoencaCoagulacao { get; set; }
    public bool DoencaEpilepsia { get; set; }
    public bool DoencaAVCTIA { get; set; }
    
    [MaxLength(2000)]
    public string? MedicacaoAtual { get; set; }
    
    [MaxLength(1000)]
    public string? SuplementosFitoterapia { get; set; }
    
    [MaxLength(100)]
    public string? Anticoagulantes { get; set; } // Não | Varfarina | DOAC | Antiagregante
    
    // Alertas/contraindicações
    public bool AlertaFraturasRecentes { get; set; }
    public bool AlertaOsteoporoseSevera { get; set; }
    public bool AlertaTumorInfecao { get; set; }
    public bool AlertaSindromesNeurologicas { get; set; }
    public bool AlertaAneurisma { get; set; }
    public bool AlertaHipermobilidade { get; set; }
    public bool AlertaCirurgiaRecente { get; set; }
    
    [MaxLength(500)]
    public string? AlertasOutros { get; set; }
    
    // ===== 5. ALERGIAS E INTOLERÂNCIAS =====
    public bool AlergiaPenicilina { get; set; }
    public bool AlergiaAINEs { get; set; }
    public bool AlergiaOpioides { get; set; }
    public bool AlergiaAnestesicos { get; set; }
    public bool AlergiaCorticosteroides { get; set; }
    public bool AlergiaIodoContraste { get; set; }
    
    [MaxLength(500)]
    public string? AlergiasFarmacosOutros { get; set; }
    
    public bool IntoleranciaGluten { get; set; }
    public bool IntoleranciaLactose { get; set; }
    public bool IntoleranciaProteínaLeite { get; set; }
    public bool IntoleranciaOvos { get; set; }
    public bool IntoleranciaMarisco { get; set; }
    public bool IntoleranciaFrutosSecos { get; set; }
    
    [MaxLength(500)]
    public string? IntoleranciaAlimentarOutras { get; set; }
    
    // Alergias ambientais específicas
    public bool AlergiaPolen { get; set; }
    public bool AlergiaAcaros { get; set; }
    public bool AlergiaPelosAnimais { get; set; }
    public bool AlergiaPoeira { get; set; }
    public bool AlergiaMofo { get; set; }
    
    [MaxLength(500)]
    public string? AlergiasAmbientaisOutras { get; set; }
    
    public bool AlergiaLatex { get; set; }
    public bool AlergiaNiquel { get; set; }
    public bool AlergiaCosmeticos { get; set; }
    public bool AlergiaAdesivos { get; set; }
    public bool AlergiaPlantas { get; set; }
    
    [MaxLength(500)]
    public string? AlergiaContactoOutras { get; set; }
    
    public bool HistoriaAnafilaxia { get; set; }
    
    [MaxLength(300)]
    public string? AnafilaxiaGatilho { get; set; }
    
    // ===== 6. HISTÓRICO CIRÚRGICO E FRATURAS =====
    public bool JaFezCirurgias { get; set; }
    
    [MaxLength(2000)]
    public string? HistoricoCirurgias { get; set; }
    
    public bool JaTeveFraturas { get; set; }
    
    [MaxLength(1000)]
    public string? HistoricoFraturas { get; set; }
    
    // ===== 7. ESTILO DE VIDA =====
    [MaxLength(50)]
    public string? ExercicioFisico { get; set; } // Nenhum | 1-2x/semana | 3-4x/semana | ≥5x/semana
    
    [MaxLength(50)]
    public string? Tabagismo { get; set; } // Nunca | Ex-fumador | Ocasional | Diário <10 | Diário ≥10
    
    [MaxLength(50)]
    public string? ConsumoAlcool { get; set; } // Nunca | Ocasional | Moderado | Frequente
    
    public int? ConsumoCafeina { get; set; } // 0-4+
    
    [MaxLength(20)]
    public string? HorasSono { get; set; } // <5 | 5-6 | 7-8 | >8
    
    [MaxLength(20)]
    public string? QualidadeSono { get; set; } // Ótima | Boa | Razoável | Má
    
    public int? NivelStress { get; set; } // 0-10
    
    [MaxLength(1000)]
    public string? ProfissaoErgonomia { get; set; }
    
    [MaxLength(1000)]
    public string? HabitosAlimentares { get; set; }
    
    // ===== 8. DIGESTIVO / METABÓLICO =====
    public bool SintomaAzia { get; set; }
    public bool SintomaDorAbdominal { get; set; }
    public bool SintomaDistensao { get; set; }
    public bool SintomaNauseas { get; set; }
    public bool SintomaVomitos { get; set; }
    public bool SintomaDiarreia { get; set; }
    public bool SintomaObstipacao { get; set; }
    public bool SintomaAlternancia { get; set; }
    public bool SintomaFezesSangue { get; set; }
    
    [MaxLength(20)]
    public string? FrequenciaEvacuacao { get; set; } // <3 | 3-5 | 6-8 | >8
    
    public int? EscalaBristol { get; set; } // 1-7
    
    [MaxLength(500)]
    public string? DiagnosticosDigestivos { get; set; }
    
    // ===== 9. CARDIO-RESPIRATÓRIO =====
    public bool HipertensaoConhecida { get; set; }
    public bool SintomaDorToracica { get; set; }
    public bool SintomaPalpitacoes { get; set; }
    public bool SintomaEdemas { get; set; }
    public bool SintomaDispneia { get; set; }
    public bool SintomaIntoleranciaEsforco { get; set; }
    public bool SintomaTosseCronica { get; set; }
    public bool SintomaPieiraAsma { get; set; }
    public bool SintomaApneiaSono { get; set; }
    
    // ===== 10. MÚSCULO-ESQUELÉTICO =====
    public bool DorCervical { get; set; }
    public bool DorDorsal { get; set; }
    public bool DorLombar { get; set; }
    public bool DorSacroiliaca { get; set; }
    public bool DorAnca { get; set; }
    public bool DorJoelho { get; set; }
    public bool DorTornozeloPe { get; set; }
    public bool DorOmbro { get; set; }
    public bool DorCotovelo { get; set; }
    public bool DorPunhoMao { get; set; }
    public bool DorATM { get; set; }
    
    [MaxLength(300)]
    public string? DorOutrasLocalizacoes { get; set; }
    
    public bool PadraoAguda { get; set; }
    public bool PadraoCronica { get; set; }
    public bool PadraoIrradiacao { get; set; }
    public bool PadraoFormigueiros { get; set; }
    public bool PadraoFraqueza { get; set; }
    public bool PadraoRigidezMatinal { get; set; }
    public bool PadraoBloqueios { get; set; }
    public bool PadraoCaibras { get; set; }
    
    // Bandeiras vermelhas
    public bool BandeiraPeridaPeso { get; set; }
    public bool BandeiraFebre { get; set; }
    public bool BandeiraTrauma { get; set; }
    public bool BandeiraDeficesNeurologicos { get; set; }
    public bool BandeiraIncontinencia { get; set; }
    public bool BandeiraCancro { get; set; }
    public bool BandeiraCorticoides { get; set; }
    
    [MaxLength(1000)]
    public string? MovimentosLimitados { get; set; }
    
    // ===== 11. NEUROLÓGICO / PSICOEMOCIONAL =====
    public bool SintomaCefaleias { get; set; }
    public bool SintomaTonturas { get; set; }
    public bool SintomaDesmaios { get; set; }
    public bool SintomaConvulsoes { get; set; }
    public bool SintomaMemoria { get; set; }
    public bool SintomaAnsiedade { get; set; }
    public bool SintomaDepressao { get; set; }
    public bool SintomaPanico { get; set; }
    public bool SintomaIdeacaoSuicida { get; set; }
    public bool AcompanhamentoPsicologico { get; set; }
    
    [MaxLength(50)]
    public string? RoncoApneia { get; set; } // Não | Ronco | Apneia suspeita | Apneia diagnosticada
    
    // ===== 12. DERMATOLÓGICO / IMUNOLÓGICO / ENDÓCRINO =====
    public bool ProblemaEczema { get; set; }
    public bool ProblemaPsoriase { get; set; }
    public bool ProblemaUrticaria { get; set; }
    public bool ProblemaAutoimune { get; set; }
    public bool ProblemaHipoHipertiroidismo { get; set; }
    public bool ProblemaDiabetes { get; set; }
    public bool ProblemaSindromeMetabolica { get; set; }
    
    [MaxLength(500)]
    public string? ProblemasOutrosDetalhes { get; set; }
    
    // ===== 13. UROLÓGICO / GINECOLÓGICO =====
    public bool ProblemaITURecorrentes { get; set; }
    public bool ProblemaDorPelvica { get; set; }
    public bool ProblemaIncontinencia { get; set; }
    public bool ProblemaHiperplasiaProstatica { get; set; }
    public bool ProblemaEndometriose { get; set; }
    public bool ProblemaSPM { get; set; }
    public bool ProblemaCicloIrregular { get; set; }
    public bool ProblemaMenupausa { get; set; }
    public bool ProblemaInfertilidade { get; set; }
    
    [MaxLength(500)]
    public string? CicloMenstrualDetalhes { get; set; }
    
    // ===== 14. RISCOS E SEGURANÇA =====
    [MaxLength(50)]
    public string? QuedasUltimoAno { get; set; } // Não | 1 queda | ≥2 quedas | Com lesão
    
    public bool SegurancaCasaRelacoes { get; set; }
    
    public bool ConsumoRiscoAlcool { get; set; }
    public bool ConsumoRiscoTabaco { get; set; }
    public bool ConsumoRiscoCannabis { get; set; }
    public bool ConsumoRiscoOutras { get; set; }
    public bool ConsumoRiscoJogo { get; set; }
    
    // ===== 15. DECLARAÇÕES FINAIS =====
    public bool DeclaracaoVeracidade { get; set; }
    public bool DeclaracaoComplementar { get; set; }
    public bool AutorizacaoPartilha { get; set; }
    public bool AutorizacaoContactos { get; set; }
    
    [MaxLength(200)]
    public string? LocalData { get; set; }
    
    public bool AssinaturaUtente { get; set; }
    public bool AssinaturaProfissional { get; set; }
    
    // ===== CAMPOS DE CONTROLO =====
    // Informações gerais de saúde
    [MaxLength(1000)]
    public string? CondicoesCronicas { get; set; }
    
    [MaxLength(1000)]
    public string? SintomasAtuais { get; set; }
    
    public bool TomaMedicacao { get; set; }
    
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