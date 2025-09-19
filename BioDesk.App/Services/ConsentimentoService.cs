using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using BioDesk.App.Models;
using BioDesk.App.Data;
using Microsoft.EntityFrameworkCore;

namespace BioDesk.App.Services
{
    public interface IConsentimentoService
    {
        Task<string> GerarConsentimentoAsync(Paciente paciente, TipoTerapiaEnum tipoTerapia, string? observacoes = null);
        Task<ConsentimentoInformado> SalvarConsentimentoAsync(ConsentimentoInformado consentimento);
        Task<List<ConsentimentoInformado>> ObterConsentimentosPacienteAsync(int pacienteId);
        string CalcularHashVerificacao(string conteudo);
        bool ValidarConsentimento(ConsentimentoInformado consentimento);
    }

    public class ConsentimentoService : IConsentimentoService
    {
        private readonly BioDeskDbContext _context;
        private readonly string _templatesPath;

        public ConsentimentoService(BioDeskDbContext context)
        {
            _context = context;
            _templatesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "utilitarios", "consentimentos");
        }

        public async Task<string> GerarConsentimentoAsync(Paciente paciente, TipoTerapiaEnum tipoTerapia, string? observacoes = null)
        {
            try
            {
                // Obter o template apropriado
                string templatePath = GetTemplatePath(tipoTerapia);
                if (!File.Exists(templatePath))
                {
                    throw new FileNotFoundException($"Template não encontrado para {tipoTerapia}: {templatePath}");
                }

                string template = await File.ReadAllTextAsync(templatePath, Encoding.UTF8);

                // Substituir placeholders
                string conteudo = SubstituirPlaceholders(template, paciente, observacoes);

                return conteudo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar consentimento: {ex.Message}", ex);
            }
        }

        public async Task<ConsentimentoInformado> SalvarConsentimentoAsync(ConsentimentoInformado consentimento)
        {
            try
            {
                // Calcular hash de verificação
                consentimento.HashVerificacao = CalcularHashVerificacao(consentimento.ConteudoConsentimento);
                consentimento.DataCriacao = DateTime.Now;

                _context.ConsentimentosInformados.Add(consentimento);
                await _context.SaveChangesAsync();

                return consentimento;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar consentimento: {ex.Message}", ex);
            }
        }

        public async Task<List<ConsentimentoInformado>> ObterConsentimentosPacienteAsync(int pacienteId)
        {
            return await _context.ConsentimentosInformados
                .Where(c => c.PacienteId == pacienteId)
                .OrderByDescending(c => c.DataConsentimento)
                .ToListAsync();
        }

        public string CalcularHashVerificacao(string conteudo)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(conteudo));
                return Convert.ToHexString(bytes).ToLower();
            }
        }

        public bool ValidarConsentimento(ConsentimentoInformado consentimento)
        {
            if (string.IsNullOrEmpty(consentimento.ConteudoConsentimento) || 
                string.IsNullOrEmpty(consentimento.HashVerificacao))
                return false;

            string hashCalculado = CalcularHashVerificacao(consentimento.ConteudoConsentimento);
            return hashCalculado.Equals(consentimento.HashVerificacao, StringComparison.OrdinalIgnoreCase);
        }

        private string GetTemplatePath(TipoTerapiaEnum tipoTerapia)
        {
            string nomeArquivo = tipoTerapia switch
            {
                TipoTerapiaEnum.Naturopatia => "naturopatia.html",
                TipoTerapiaEnum.Osteopatia => "osteopatia.html",
                TipoTerapiaEnum.MedicinaQuantica => "medicina-quantica.html",
                TipoTerapiaEnum.Mesoterapia => "mesoterapia.html",
                TipoTerapiaEnum.Homeopatia => "homeopatia.html",
                TipoTerapiaEnum.Acupunctura => "acupunctura.html",
                TipoTerapiaEnum.Fitoterapia => "fitoterapia.html",
                TipoTerapiaEnum.Reflexologia => "reflexologia.html",
                TipoTerapiaEnum.MassagemTerapeutica => "massagem-terapeutica.html",
                _ => "template-generico.html"
            };

            return Path.Combine(_templatesPath, nomeArquivo);
        }

        private string SubstituirPlaceholders(string template, Paciente paciente, string? observacoes)
        {
            var substituicoes = new Dictionary<string, string>
            {
                { "{{NOME_PACIENTE}}", paciente.NomeCompleto },
                { "{{DATA_NASCIMENTO}}", paciente.DataNascimento.ToString("dd/MM/yyyy") },
                { "{{TELEFONE}}", paciente.Telefone ?? "Não informado" },
                { "{{EMAIL}}", paciente.Email ?? "Não informado" },
                { "{{DATA_CONSENTIMENTO}}", DateTime.Now.ToString("dd/MM/yyyy") },
                { "{{LOCAL_TRATAMENTO}}", "Consultório BioDesk PRO" }, // Pode ser configurável
                { "{{IDENTIFICACAO_PROFISSIONAL}}", "Dr. [Nome do Profissional]" }, // Pode ser configurável
                { "{{OBSERVACOES_ADICIONAIS}}", observacoes ?? "Nenhuma observação adicional." },
                { "{{HASH_VERIFICACAO}}", "{{HASH_SERA_CALCULADO}}" } // Será substituído após cálculo
            };

            string resultado = template;
            foreach (var kvp in substituicoes)
            {
                resultado = resultado.Replace(kvp.Key, kvp.Value);
            }

            return resultado;
        }

        public List<TipoTerapiaEnum> ObterTerapiasDisponiveis()
        {
            return new List<TipoTerapiaEnum>
            {
                TipoTerapiaEnum.Naturopatia,
                TipoTerapiaEnum.Osteopatia,
                TipoTerapiaEnum.MedicinaQuantica,
                TipoTerapiaEnum.Mesoterapia,
                TipoTerapiaEnum.Homeopatia,
                TipoTerapiaEnum.Acupunctura,
                TipoTerapiaEnum.Fitoterapia,
                TipoTerapiaEnum.Reflexologia,
                TipoTerapiaEnum.MassagemTerapeutica
            };
        }

        public string ObterDescricaoTerapia(TipoTerapiaEnum tipo)
        {
            return tipo switch
            {
                TipoTerapiaEnum.Naturopatia => "Medicina natural com foco em plantas medicinais e nutrição",
                TipoTerapiaEnum.Osteopatia => "Terapia manual para sistema musculoesquelético",
                TipoTerapiaEnum.MedicinaQuantica => "Terapia energética baseada em princípios quânticos",
                TipoTerapiaEnum.Mesoterapia => "Microinjeções para tratamentos localizados",
                TipoTerapiaEnum.Homeopatia => "Sistema terapêutico baseado na lei dos semelhantes",
                TipoTerapiaEnum.Acupunctura => "Medicina tradicional chinesa com agulhas",
                TipoTerapiaEnum.Fitoterapia => "Tratamento através de plantas medicinais",
                TipoTerapiaEnum.Reflexologia => "Terapia através de pontos reflexos nos pés",
                TipoTerapiaEnum.MassagemTerapeutica => "Técnicas de massagem para fins terapêuticos",
                _ => "Terapia complementar"
            };
        }
    }
}