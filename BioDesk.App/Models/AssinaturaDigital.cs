using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioDesk.App.Models
{
    public class AssinaturaDigital
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public virtual Paciente Paciente { get; set; } = null!;

        public int? QuestionarioSaudeId { get; set; }

        [ForeignKey("QuestionarioSaudeId")]
        public virtual QuestionarioSaude? QuestionarioSaude { get; set; }

        public int? ConsentimentoInformadoId { get; set; }

        [ForeignKey("ConsentimentoInformadoId")]
        public virtual ConsentimentoInformado? ConsentimentoInformado { get; set; }

        [Required]
        [MaxLength(100)]
        public string TipoDocumento { get; set; } = string.Empty; // "QuestionarioSaude", "ConsentimentoNaturopatia", etc.

        [Required]
        public byte[] DadosAssinatura { get; set; } = Array.Empty<byte>(); // Stroke data

        [Required]
        public byte[] ImagemAssinatura { get; set; } = Array.Empty<byte>(); // PNG image

        [Required]
        public DateTime DataAssinatura { get; set; }

        [Required]
        [MaxLength(500)]
        public string HashVerificacao { get; set; } = string.Empty; // Hash dos dados + timestamp

        public int NumeroTracos { get; set; }

        public double LarguraAssinatura { get; set; }

        public double AlturaAssinatura { get; set; }

        [MaxLength(200)]
        public string? DispositivoUtilizado { get; set; } // "Mouse", "Touch", "Stylus", etc.

        [MaxLength(100)]
        public string? VersaoApp { get; set; }

        [MaxLength(1000)]
        public string? ObservacoesValidacao { get; set; }

        public bool AssinaturaValida { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime? DataUltimaVerificacao { get; set; }
    }
}