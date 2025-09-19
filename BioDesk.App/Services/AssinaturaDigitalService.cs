using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BioDesk.App.Data;
using BioDesk.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BioDesk.App.Services
{
    public class AssinaturaDigitalService
    {
        private readonly BioDeskDbContext _context;

        public AssinaturaDigitalService(BioDeskDbContext context)
        {
            _context = context;
        }

        public async Task<AssinaturaDigital> SalvarAssinaturaAsync(
            int pacienteId,
            string tipoDocumento,
            byte[] dadosAssinatura,
            BitmapSource imagemAssinatura,
            DateTime dataAssinatura,
            int numeroTracos,
            double largura,
            double altura,
            string? dispositivo = null,
            int? questionarioSaudeId = null,
            int? consentimentoInformadoId = null)
        {
            try
            {
                // Converter imagem para byte array
                var imagemBytes = ConvertBitmapToByteArray(imagemAssinatura);

                // Gerar hash de verificação
                var hashVerificacao = GerarHashVerificacao(dadosAssinatura, imagemBytes, dataAssinatura, pacienteId);

                var assinatura = new AssinaturaDigital
                {
                    PacienteId = pacienteId,
                    QuestionarioSaudeId = questionarioSaudeId,
                    ConsentimentoInformadoId = consentimentoInformadoId,
                    TipoDocumento = tipoDocumento,
                    DadosAssinatura = dadosAssinatura,
                    ImagemAssinatura = imagemBytes,
                    DataAssinatura = dataAssinatura,
                    HashVerificacao = hashVerificacao,
                    NumeroTracos = numeroTracos,
                    LarguraAssinatura = largura,
                    AlturaAssinatura = altura,
                    DispositivoUtilizado = dispositivo ?? DetectarDispositivo(),
                    VersaoApp = GetVersaoApp(),
                    AssinaturaValida = true,
                    DataCriacao = DateTime.Now
                };

                _context.AssinaturasDigitais.Add(assinatura);
                await _context.SaveChangesAsync();

                return assinatura;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao salvar assinatura digital: {ex.Message}", ex);
            }
        }

        public async Task<AssinaturaDigital?> ObterAssinaturaAsync(int id)
        {
            return await _context.AssinaturasDigitais
                .Include(a => a.Paciente)
                .Include(a => a.QuestionarioSaude)
                .Include(a => a.ConsentimentoInformado)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AssinaturaDigital?> ObterAssinaturaPorDocumentoAsync(
            int pacienteId, 
            string tipoDocumento, 
            int? documentoId = null)
        {
            var query = _context.AssinaturasDigitais
                .Include(a => a.Paciente)
                .Where(a => a.PacienteId == pacienteId && a.TipoDocumento == tipoDocumento);

            if (documentoId.HasValue)
            {
                if (tipoDocumento == "QuestionarioSaude")
                {
                    query = query.Where(a => a.QuestionarioSaudeId == documentoId.Value);
                }
                else if (tipoDocumento.StartsWith("Consentimento"))
                {
                    query = query.Where(a => a.ConsentimentoInformadoId == documentoId.Value);
                }
            }

            return await query
                .OrderByDescending(a => a.DataAssinatura)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ValidarAssinaturaAsync(int assinaturaId)
        {
            var assinatura = await ObterAssinaturaAsync(assinaturaId);
            if (assinatura == null) return false;

            try
            {
                // Recalcular hash e comparar
                var hashCalculado = GerarHashVerificacao(
                    assinatura.DadosAssinatura,
                    assinatura.ImagemAssinatura,
                    assinatura.DataAssinatura,
                    assinatura.PacienteId);

                var hashValido = hashCalculado == assinatura.HashVerificacao;

                // Validações adicionais
                var temDados = assinatura.DadosAssinatura?.Length > 0;
                var temImagem = assinatura.ImagemAssinatura?.Length > 0;
                var dataValida = assinatura.DataAssinatura <= DateTime.Now;
                var tracosValidos = assinatura.NumeroTracos > 0;

                var assinaturaValida = hashValido && temDados && temImagem && dataValida && tracosValidos;

                // Atualizar status se necessário
                if (assinatura.AssinaturaValida != assinaturaValida)
                {
                    assinatura.AssinaturaValida = assinaturaValida;
                    assinatura.DataUltimaVerificacao = DateTime.Now;
                    assinatura.ObservacoesValidacao = assinaturaValida 
                        ? "Assinatura validada com sucesso" 
                        : $"Falha na validação: Hash={hashValido}, Dados={temDados}, Imagem={temImagem}, Data={dataValida}, Traços={tracosValidos}";
                    
                    await _context.SaveChangesAsync();
                }

                return assinaturaValida;
            }
            catch (Exception ex)
            {
                // Log do erro e marcar como inválida
                var assinaturaEntity = await _context.AssinaturasDigitais.FindAsync(assinaturaId);
                if (assinaturaEntity != null)
                {
                    assinaturaEntity.AssinaturaValida = false;
                    assinaturaEntity.DataUltimaVerificacao = DateTime.Now;
                    assinaturaEntity.ObservacoesValidacao = $"Erro na validação: {ex.Message}";
                    await _context.SaveChangesAsync();
                }

                return false;
            }
        }

        public async Task<BitmapSource?> ObterImagemAssinaturaAsync(int assinaturaId)
        {
            var assinatura = await _context.AssinaturasDigitais
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == assinaturaId);

            if (assinatura?.ImagemAssinatura == null || assinatura.ImagemAssinatura.Length == 0)
                return null;

            return ConvertByteArrayToBitmap(assinatura.ImagemAssinatura);
        }

        public async Task<bool> RemoverAssinaturaAsync(int assinaturaId)
        {
            try
            {
                var assinatura = await _context.AssinaturasDigitais.FindAsync(assinaturaId);
                if (assinatura == null) return false;

                _context.AssinaturasDigitais.Remove(assinatura);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GerarHashVerificacao(byte[] dadosAssinatura, byte[] imagemAssinatura, DateTime dataAssinatura, int pacienteId)
        {
            using var sha256 = SHA256.Create();
            
            var combinedData = new List<byte>();
            combinedData.AddRange(dadosAssinatura);
            combinedData.AddRange(imagemAssinatura);
            combinedData.AddRange(BitConverter.GetBytes(dataAssinatura.ToBinary()));
            combinedData.AddRange(BitConverter.GetBytes(pacienteId));
            
            var hash = sha256.ComputeHash(combinedData.ToArray());
            return Convert.ToBase64String(hash);
        }

        private static byte[] ConvertBitmapToByteArray(BitmapSource bitmap)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            
            using var stream = new MemoryStream();
            encoder.Save(stream);
            return stream.ToArray();
        }

        private static BitmapSource ConvertByteArrayToBitmap(byte[] data)
        {
            using var stream = new MemoryStream(data);
            var decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            return decoder.Frames[0];
        }

        private static string DetectarDispositivo()
        {
            // Detectar tipo de dispositivo usado para assinatura
            // Por simplicidade, retornamos "Mouse" por padrão
            // Em uma implementação mais avançada, poderia detectar touch/stylus
            return "Mouse";
        }

        private static string GetVersaoApp()
        {
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var version = assembly.GetName().Version;
                return version?.ToString() ?? "1.0.0.0";
            }
            catch
            {
                return "1.0.0.0";
            }
        }
    }
}