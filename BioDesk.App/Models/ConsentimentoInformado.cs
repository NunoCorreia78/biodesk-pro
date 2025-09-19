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
    }

    public enum TipoTerapiaEnum
    {
        Naturopatia,
        Osteopatia,
        MedicinaQuantica,
        Mesoterapia,
        Homeopatia,
        Acupunctura,
        Fitoterapia,
        Reflexologia,
        MassagemTerapeutica,
        Outro
    }
}