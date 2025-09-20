using System;
using System.ComponentModel.DataAnnotations;

namespace BioDesk.App.Models
{
    public class ConsentimentoInformado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string TipoTerapia { get; set; } = string.Empty; // Naturopatia, Osteopatia, etc.

        [Required]
        public string ConteudoConsentimento { get; set; } = string.Empty;

        public string? AssinaturaDigital { get; set; } // Base64 da assinatura

        public DateTime DataConsentimento { get; set; }

        public bool ConsentimentoObtido { get; set; }

        [StringLength(500)]
        public string? ObservacoesAdicionais { get; set; }

        // Campos específicos para proteção legal
        public bool AceitaContactoFisico { get; set; }
        public bool CompreendeNaturezaTratamento { get; set; }
        public bool AceitaRiscosAssociados { get; set; }
        public bool PermiteExposicaoAreasCorporais { get; set; }

        [StringLength(1000)]
        public string? TermosEspecificos { get; set; }

        // Identificação da sessão e profissional
        [StringLength(200)]
        public string? IdentificacaoProfissional { get; set; }

        [StringLength(100)]
        public string? LocalTratamento { get; set; }

        public DateTime? DataValidade { get; set; }

        // Metadados
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataUltimaAtualizacao { get; set; }

        [StringLength(500)]
        public string? CaminhoDocumento { get; set; } // Path do PDF gerado

        public string? HashVerificacao { get; set; } // Hash para verificar integridade

        // Novos campos para consentimentos específicos
        
        // Naturopatia
        public bool NaturopatiaCompreendeNatureza { get; set; }
        public bool NaturopatiaObjetivosExplicados { get; set; }
        public bool NaturopatiaRiscosDiscutidos { get; set; }
        public bool NaturopatiaInformacaoPatologias { get; set; }
        public bool NaturopatiaAlternativasDiscutidas { get; set; }
        public bool NaturopatiaAutorizacaoCorresponsabilidade { get; set; }
        public bool NaturopatiaRevogavel { get; set; }
        public string? NaturopatiaAssinatura { get; set; }
        public DateTime? NaturopatiaDataAssinatura { get; set; }

        // Osteopatia
        public bool OsteopatiaTecnicasExplicadas { get; set; }
        public bool OsteopatiaContraindicacoesDiscutidas { get; set; }
        public bool OsteopatiaRiscosExplicados { get; set; }
        public bool OsteopatiaAutorizoContactoFisico { get; set; }
        public bool OsteopatiaPossoInterromper { get; set; }
        public bool OsteopatiaRevogavel { get; set; }
        public string? OsteopatiaAssinatura { get; set; }
        public DateTime? OsteopatiaDataAssinatura { get; set; }

        // Iridologia
        public bool IridologiaNaturezaNaoInvasiva { get; set; }
        public bool IridologiaAutorizoCapturaImagens { get; set; }
        public bool IridologiaCompreensoLimitacoes { get; set; }
        public bool IridologiaRevogavel { get; set; }
        public string? IridologiaAssinatura { get; set; }
        public DateTime? IridologiaDataAssinatura { get; set; }

        // Medicina Quântica
        public bool MedicinaQuanticaAbordagemComplementar { get; set; }
        public bool MedicinaQuanticaNaturezaProcedimentos { get; set; }
        public bool MedicinaQuanticaPossiveisReacoes { get; set; }
        public bool MedicinaQuanticaContraindicacoes { get; set; }
        public bool MedicinaQuanticaLiberdadeInterromper { get; set; }
        public bool MedicinaQuanticaRevogavel { get; set; }
        public string? MedicinaQuanticaAssinatura { get; set; }
        public DateTime? MedicinaQuanticaDataAssinatura { get; set; }

        // RGPD
        public bool RgpdInformacaoResponsavel { get; set; }
        public bool RgpdDireitosAcesso { get; set; }
        public bool RgpdConsentimentoExplicito { get; set; }
        public bool RgpdOpcaoNaoMarketing { get; set; }
        public bool RgpdRevogavel { get; set; }
        public string? RgpdAssinatura { get; set; }
        public DateTime? RgpdDataAssinatura { get; set; }

        // Assinatura geral do questionário
        public string? AssinaturaQuestionarioCompleto { get; set; }
        public DateTime? DataAssinaturaQuestionario { get; set; }
        public bool QuestionarioAssinado { get; set; }
    }

    public enum TipoTerapiaEnum
    {
        Naturopatia,
        Osteopatia,
        Iridologia,
        MedicinaQuantica,
        Mesoterapia,
        Homeopatia,
        Acupunctura,
        Fitoterapia,
        Reflexologia,
        MassagemTerapeutica,
        RGPD,
        Outro
    }
}