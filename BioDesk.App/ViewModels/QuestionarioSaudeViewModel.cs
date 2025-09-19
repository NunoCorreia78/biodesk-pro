using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BioDesk.App.Data;
using BioDesk.App.Models;
using BioDesk.App.Services;
using BioDesk.App.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace BioDesk.App.ViewModels
{
    public partial class QuestionarioSaudeViewModel : ViewModelBase
    {
        private readonly BioDeskDbContext _context;
        private readonly AssinaturaDigitalService _assinaturaService;
        private Paciente? _pacienteSelecionado;
        private QuestionarioSaude? _questionarioAtual;

        [ObservableProperty]
        private string condicoesCronicas = string.Empty;

        [ObservableProperty]
        private string sintomasAtuais = string.Empty;

        [ObservableProperty]
        private string medicacaoAtual = string.Empty;

        [ObservableProperty]
        private string alergiasAlimentos = string.Empty;

        [ObservableProperty]
        private string alergiasMedicamentos = string.Empty;

        [ObservableProperty]
        private string alergiasAmbientais = string.Empty;

        [ObservableProperty]
        private string alergiasPlantasSupl = string.Empty;

        [ObservableProperty]
        private string historicoCirurgias = string.Empty;

        [ObservableProperty]
        private string historicoFraturas = string.Empty;

        [ObservableProperty]
        private string detalhesEstiloVida = string.Empty;

        [ObservableProperty]
        private bool fuma = false;

        [ObservableProperty]
        private bool consomeAlcool = false;

        [ObservableProperty]
        private bool praticaExercicio = false;

        [ObservableProperty]
        private string historicoFamiliar = string.Empty;

        [ObservableProperty]
        private string historicoGinecologico = string.Empty;

        [ObservableProperty]
        private string detalhesProblemasSaude = string.Empty;

        [ObservableProperty]
        private string observacoesAdicionais = string.Empty;

        [ObservableProperty]
        private bool isLoading = false;

        [ObservableProperty]
        private bool hasUnsavedChanges = false;

        [ObservableProperty]
        private DateTime? dataPreenchimento;

        [ObservableProperty]
        private bool temAssinatura = false;

        [ObservableProperty]
        private DateTime? dataAssinatura;

        public QuestionarioSaudeViewModel(BioDeskDbContext context)
        {
            _context = context;
            _assinaturaService = new AssinaturaDigitalService(context);
            GuardarQuestionarioCommand = new AsyncRelayCommand(GuardarQuestionarioAsync, () => _pacienteSelecionado != null && !IsLoading);
            GerarConsentimentoCommand = new AsyncRelayCommand(GerarConsentimentoAsync, () => _pacienteSelecionado != null && !IsLoading);
            
            // Monitorar mudanças para indicar dados não salvos
            PropertyChanged += OnPropertyChanged;
        }

        public IAsyncRelayCommand GuardarQuestionarioCommand { get; }
        public IAsyncRelayCommand GerarConsentimentoCommand { get; }

        public void SetPaciente(Paciente? paciente)
        {
            _pacienteSelecionado = paciente;
            if (paciente != null)
            {
                _ = CarregarQuestionarioAsync(paciente.Id);
            }
            else
            {
                LimparFormulario();
            }
            
            GuardarQuestionarioCommand.NotifyCanExecuteChanged();
            GerarConsentimentoCommand.NotifyCanExecuteChanged();
        }

        private async Task CarregarQuestionarioAsync(int pacienteId)
        {
            try
            {
                IsLoading = true;
                
                _questionarioAtual = await _context.QuestionariosSaude
                    .FirstOrDefaultAsync(q => q.PacienteId == pacienteId);

                if (_questionarioAtual != null)
                {
                    // Carregar dados existentes
                    CondicoesCronicas = _questionarioAtual.CondicoesCronicas ?? string.Empty;
                    SintomasAtuais = _questionarioAtual.SintomasAtuais ?? string.Empty;
                    MedicacaoAtual = _questionarioAtual.MedicacaoAtual ?? string.Empty;
                    AlergiasAlimentos = _questionarioAtual.AlergiasAlimentos ?? string.Empty;
                    AlergiasMedicamentos = _questionarioAtual.AlergiasMedicamentos ?? string.Empty;
                    AlergiasAmbientais = _questionarioAtual.AlergiasAmbientais ?? string.Empty;
                    AlergiasPlantasSupl = _questionarioAtual.AlergiasPlantas ?? string.Empty;
                    HistoricoCirurgias = _questionarioAtual.HistoricoCirurgias ?? string.Empty;
                    HistoricoFraturas = _questionarioAtual.HistoricoFraturas ?? string.Empty;
                    PraticaExercicio = _questionarioAtual.PraticaExercicio;
                    Fuma = _questionarioAtual.Fuma;
                    ConsomeAlcool = _questionarioAtual.ConsomeAlcool;
                    DetalhesEstiloVida = _questionarioAtual.DetalhesEstiloVida ?? string.Empty;
                    HistoricoFamiliar = _questionarioAtual.HistoricoFamiliar ?? string.Empty;
                    HistoricoGinecologico = _questionarioAtual.HistoricoGinecologico ?? string.Empty;
                    DetalhesProblemasSaude = _questionarioAtual.DetalhesProblemasSaude ?? string.Empty;
                    ObservacoesAdicionais = _questionarioAtual.ObservacoesAdicionais ?? string.Empty;
                    DataPreenchimento = _questionarioAtual.DataPreenchimento;
                    
                    // Carregar assinatura se existir
                    await CarregarAssinaturaAsync();
                }
                else
                {
                    LimparFormulario();
                }

                HasUnsavedChanges = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar questionário: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
                GerarConsentimentoCommand.NotifyCanExecuteChanged();
            }
        }

        private async Task GuardarQuestionarioAsync()
        {
            if (_pacienteSelecionado == null) return;

            try
            {
                IsLoading = true;

                if (_questionarioAtual == null)
                {
                    _questionarioAtual = new QuestionarioSaude
                    {
                        PacienteId = _pacienteSelecionado.Id,
                        DataPreenchimento = DateTime.Now
                    };
                    _context.QuestionariosSaude.Add(_questionarioAtual);
                }

                // Atualizar dados
                _questionarioAtual.CondicoesCronicas = CondicoesCronicas;
                _questionarioAtual.SintomasAtuais = SintomasAtuais;
                _questionarioAtual.MedicacaoAtual = MedicacaoAtual;
                _questionarioAtual.AlergiasAlimentos = AlergiasAlimentos;
                _questionarioAtual.AlergiasMedicamentos = AlergiasMedicamentos;
                _questionarioAtual.AlergiasAmbientais = AlergiasAmbientais;
                _questionarioAtual.AlergiasPlantas = AlergiasPlantasSupl;
                _questionarioAtual.HistoricoCirurgias = HistoricoCirurgias;
                _questionarioAtual.HistoricoFraturas = HistoricoFraturas;
                _questionarioAtual.PraticaExercicio = PraticaExercicio;
                _questionarioAtual.Fuma = Fuma;
                _questionarioAtual.ConsomeAlcool = ConsomeAlcool;
                _questionarioAtual.DetalhesEstiloVida = DetalhesEstiloVida;
                _questionarioAtual.HistoricoFamiliar = HistoricoFamiliar;
                _questionarioAtual.HistoricoGinecologico = HistoricoGinecologico;
                _questionarioAtual.DetalhesProblemasSaude = DetalhesProblemasSaude;
                _questionarioAtual.ObservacoesAdicionais = ObservacoesAdicionais;
                _questionarioAtual.DataUltimaAtualizacao = DateTime.Now;

                await _context.SaveChangesAsync();

                DataPreenchimento = _questionarioAtual.DataPreenchimento;
                HasUnsavedChanges = false;

                MessageBox.Show("Questionário guardado com sucesso!", 
                                   "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao guardar questionário: {ex.Message}", 
                                   "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
                GerarConsentimentoCommand.NotifyCanExecuteChanged();
            }
        }

        private async Task GerarConsentimentoAsync()
        {
            if (_pacienteSelecionado == null) 
            {
                MessageBox.Show("Selecione um paciente primeiro.", 
                               "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                IsLoading = true;

                // Verificar se há dados suficientes
                if (string.IsNullOrWhiteSpace(CondicoesCronicas) && 
                    string.IsNullOrWhiteSpace(MedicacaoAtual) &&
                    string.IsNullOrWhiteSpace(AlergiasAlimentos) &&
                    string.IsNullOrWhiteSpace(AlergiasMedicamentos))
                {
                    MessageBox.Show("Complete pelo menos as informações básicas antes de gerar o consentimento.", 
                                       "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Implementar geração real do documento de consentimento
                var consentimentoService = new ConsentimentoService(_context);
                
                // Selecionar tipo de terapia (por agora, usar naturopatia como padrão)
                var tipoTerapia = Models.TipoTerapiaEnum.Naturopatia;
                var observacoes = $"Questionário preenchido em {DateTime.Now:dd/MM/yyyy HH:mm}";
                
                try 
                {
                    var caminhoDocumento = await consentimentoService.GerarConsentimentoAsync(
                        _pacienteSelecionado, 
                        tipoTerapia, 
                        observacoes);
                    
                    MessageBox.Show($"Documento de consentimento gerado com sucesso!\n\nLocalização: {caminhoDocumento}", 
                                   "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao gerar documento: {ex.Message}", 
                                   "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar consentimento: {ex.Message}", 
                                   "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void LimparFormulario()
        {
            CondicoesCronicas = string.Empty;
            SintomasAtuais = string.Empty;
            MedicacaoAtual = string.Empty;
            AlergiasAlimentos = string.Empty;
            AlergiasMedicamentos = string.Empty;
            AlergiasAmbientais = string.Empty;
            AlergiasPlantasSupl = string.Empty;
            HistoricoCirurgias = string.Empty;
            HistoricoFraturas = string.Empty;
            DetalhesEstiloVida = string.Empty;
            Fuma = false;
            ConsomeAlcool = false;
            PraticaExercicio = false;
            HistoricoFamiliar = string.Empty;
            HistoricoGinecologico = string.Empty;
            DetalhesProblemasSaude = string.Empty;
            ObservacoesAdicionais = string.Empty;
            DataPreenchimento = null;
            TemAssinatura = false;
            DataAssinatura = null;
            HasUnsavedChanges = false;
            _questionarioAtual = null;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // Propriedades que não devem marcar como alterado
            if (e.PropertyName == nameof(IsLoading) || 
                e.PropertyName == nameof(HasUnsavedChanges) ||
                e.PropertyName == nameof(DataPreenchimento) ||
                e.PropertyName == nameof(TemAssinatura) ||
                e.PropertyName == nameof(DataAssinatura))
                return;

            if (!IsLoading && _questionarioAtual != null)
            {
                HasUnsavedChanges = true;
            }
        }

        public bool ValidarDados()
        {
            // Validação básica - pode ser expandida conforme necessário
            return !string.IsNullOrWhiteSpace(CondicoesCronicas) ||
                   !string.IsNullOrWhiteSpace(MedicacaoAtual) ||
                   !string.IsNullOrWhiteSpace(AlergiasAlimentos) ||
                   !string.IsNullOrWhiteSpace(AlergiasMedicamentos);
        }

        public async Task ProcessarAssinaturaAsync(byte[] dadosAssinatura, System.Windows.Media.Imaging.BitmapSource imagemAssinatura, int numeroTracos, double largura, double altura)
        {
            if (_questionarioAtual == null || _pacienteSelecionado == null)
            {
                MessageBox.Show("Primeiro deve guardar o questionário antes de assinar.", 
                               "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                IsLoading = true;

                var assinatura = await _assinaturaService.SalvarAssinaturaAsync(
                    _pacienteSelecionado.Id,
                    "QuestionarioSaude",
                    dadosAssinatura,
                    imagemAssinatura,
                    DateTime.Now,
                    numeroTracos,
                    largura,
                    altura,
                    "Canvas Digital",
                    _questionarioAtual.Id,
                    null);

                TemAssinatura = true;
                DataAssinatura = assinatura.DataAssinatura;

                MessageBox.Show("Assinatura registada com sucesso!", 
                               "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar assinatura: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task CarregarAssinaturaAsync()
        {
            if (_questionarioAtual == null || _pacienteSelecionado == null) return;

            try
            {
                var assinatura = await _assinaturaService.ObterAssinaturaPorDocumentoAsync(
                    _pacienteSelecionado.Id, 
                    "QuestionarioSaude", 
                    _questionarioAtual.Id);

                if (assinatura != null)
                {
                    TemAssinatura = true;
                    DataAssinatura = assinatura.DataAssinatura;
                }
                else
                {
                    TemAssinatura = false;
                    DataAssinatura = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar assinatura: {ex.Message}");
                TemAssinatura = false;
                DataAssinatura = null;
            }
        }
    }
}