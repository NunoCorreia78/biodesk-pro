using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BioDesk.App.Data;
using BioDesk.App.Models;
using BioDesk.App.Services;
using BioDesk.App.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace BioDesk.App.ViewModels
{
    public class QuestionarioCompletoViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private bool _isLoading;
        private int _pacienteId;

        public QuestionarioCompletoViewModel(INavigationService navigationService)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("QuestionarioCompletoViewModel: Construtor iniciado");
                _navigationService = navigationService;
                System.Diagnostics.Debug.WriteLine("QuestionarioCompletoViewModel: NavigationService atribuído");
                InitializeCommands();
                System.Diagnostics.Debug.WriteLine("QuestionarioCompletoViewModel: Commands inicializados");
                System.Diagnostics.Debug.WriteLine("QuestionarioCompletoViewModel: Construtor concluído com sucesso");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERRO no construtor QuestionarioCompletoViewModel: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                throw; // Re-throw to preserve original exception
            }
        }

        #region Properties

        public int PacienteId
        {
            get => _pacienteId;
            set => SetProperty(ref _pacienteId, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        // Propriedades para interface
        private string _nomePaciente = "Paciente de Teste";
        public string NomePaciente
        {
            get => _nomePaciente;
            set => SetProperty(ref _nomePaciente, value);
        }

        private DateTime _dataPreenchimento = DateTime.Now;
        public DateTime DataPreenchimento
        {
            get => _dataPreenchimento;
            set => SetProperty(ref _dataPreenchimento, value);
        }

        private bool _temQuestionario = true;
        public bool TemQuestionario
        {
            get => _temQuestionario;
            set => SetProperty(ref _temQuestionario, value);
        }

        private bool _mostrarMensagemVazia = false;
        public bool MostrarMensagemVazia
        {
            get => _mostrarMensagemVazia;
            set => SetProperty(ref _mostrarMensagemVazia, value);
        }

        private string _statusTexto = "Em desenvolvimento";
        public string StatusTexto
        {
            get => _statusTexto;
            set => SetProperty(ref _statusTexto, value);
        }

        private string _statusBackground = "#4F7C6D";
        public string StatusBackground
        {
            get => _statusBackground;
            set => SetProperty(ref _statusBackground, value);
        }

        private string _statusForeground = "White";
        public string StatusForeground
        {
            get => _statusForeground;
            set => SetProperty(ref _statusForeground, value);
        }

        private string _statusAssinaturaUtente = "⏳ Pendente";
        public string StatusAssinaturaUtente
        {
            get => _statusAssinaturaUtente;
            set => SetProperty(ref _statusAssinaturaUtente, value);
        }

        private string _statusAssinaturaProfissional = "⏳ Pendente";
        public string StatusAssinaturaProfissional
        {
            get => _statusAssinaturaProfissional;
            set => SetProperty(ref _statusAssinaturaProfissional, value);
        }

        private DateTime _dataUltimaEdicao = DateTime.Now;
        public DateTime DataUltimaEdicao
        {
            get => _dataUltimaEdicao;
            set => SetProperty(ref _dataUltimaEdicao, value);
        }

        private string _versionText = "v1.0";
        public string VersionText
        {
            get => _versionText;
            set => SetProperty(ref _versionText, value);
        }

        private bool _temAlteracoes = false;
        public bool TemAlteracoes
        {
            get => _temAlteracoes;
            set => SetProperty(ref _temAlteracoes, value);
        }

        private DateTime? _ultimoAutoSave = DateTime.Now;
        public DateTime? UltimoAutoSave
        {
            get => _ultimoAutoSave;
            set => SetProperty(ref _ultimoAutoSave, value);
        }

        private bool _podeAssinarUtente = true;
        public bool PodeAssinarUtente
        {
            get => _podeAssinarUtente;
            set => SetProperty(ref _podeAssinarUtente, value);
        }

        private bool _podeAssinarProfissional = true;
        public bool PodeAssinarProfissional
        {
            get => _podeAssinarProfissional;
            set => SetProperty(ref _podeAssinarProfissional, value);
        }

        // Propriedades para campos do formulário
        private string _doencaCronica = string.Empty;
        public string DoencaCronica
        {
            get => _doencaCronica;
            set => SetProperty(ref _doencaCronica, value);
        }

        private string _sintomasAtuais = string.Empty;
        public string SintomasAtuais
        {
            get => _sintomasAtuais;
            set => SetProperty(ref _sintomasAtuais, value);
        }

        private string _medicacaoAtual = string.Empty;
        public string MedicacaoAtual
        {
            get => _medicacaoAtual;
            set => SetProperty(ref _medicacaoAtual, value);
        }

        private string _alergiasAlimentares = string.Empty;
        public string AlergiasAlimentares
        {
            get => _alergiasAlimentares;
            set => SetProperty(ref _alergiasAlimentares, value);
        }

        private string _alergiasMedicamentos = string.Empty;
        public string AlergiasMedicamentos
        {
            get => _alergiasMedicamentos;
            set => SetProperty(ref _alergiasMedicamentos, value);
        }

        private string _alergiasAmbientais = string.Empty;
        public string AlergiasAmbientais
        {
            get => _alergiasAmbientais;
            set => SetProperty(ref _alergiasAmbientais, value);
        }

        private string _outrasAlergias = string.Empty;
        public string OutrasAlergias
        {
            get => _outrasAlergias;
            set => SetProperty(ref _outrasAlergias, value);
        }

        private string _historicoCirurgico = string.Empty;
        public string HistoricoCirurgico
        {
            get => _historicoCirurgico;
            set => SetProperty(ref _historicoCirurgico, value);
        }

        private string _fraturas = string.Empty;
        public string Fraturas
        {
            get => _fraturas;
            set => SetProperty(ref _fraturas, value);
        }

        private string _atividadeFisica = string.Empty;
        public string AtividadeFisica
        {
            get => _atividadeFisica;
            set => SetProperty(ref _atividadeFisica, value);
        }

        private string _tabagismo = string.Empty;
        public string Tabagismo
        {
            get => _tabagismo;
            set => SetProperty(ref _tabagismo, value);
        }

        private string _consumoAlcool = string.Empty;
        public string ConsumoAlcool
        {
            get => _consumoAlcool;
            set => SetProperty(ref _consumoAlcool, value);
        }

        private string _habitosAlimentares = string.Empty;
        public string HabitosAlimentares
        {
            get => _habitosAlimentares;
            set => SetProperty(ref _habitosAlimentares, value);
        }

        #endregion

        #region Methods

        public void SetPaciente(int pacienteId, string nomePaciente)
        {
            PacienteId = pacienteId;
            NomePaciente = nomePaciente;
            App.DebugLog($"QuestionarioCompletoViewModel: Paciente definido - ID: {pacienteId}, Nome: {nomePaciente}");
        }

        #endregion

        #region Commands

        public ICommand NovoQuestionarioCommand { get; private set; } = null!;
        public ICommand CarregarQuestionarioCommand { get; private set; } = null!;
        public ICommand GuardarCommand { get; private set; } = null!;
        public ICommand AssinarUtenteCommand { get; private set; } = null!;
        public ICommand AssinarProfissionalCommand { get; private set; } = null!;
        public ICommand ImprimirCommand { get; private set; } = null!;
        public ICommand ExportarPdfCommand { get; private set; } = null!;

        private void InitializeCommands()
        {
            App.DebugLog("=== INICIALIZANDO COMMANDS DO QUESTIONÁRIO ===");
            NovoQuestionarioCommand = new RelayCommand(async () => await CriarNovoQuestionario());
            CarregarQuestionarioCommand = new RelayCommand(async () => await CarregarQuestionarioExistente());
            GuardarCommand = new RelayCommand(async () => await GuardarQuestionario());
            AssinarUtenteCommand = new RelayCommand(async () => await AssinarComoUtente());
            AssinarProfissionalCommand = new RelayCommand(async () => await AssinarComoProfissional());
            ImprimirCommand = new RelayCommand(ImprimirQuestionario);
            ExportarPdfCommand = new RelayCommand(async () => await ExportarParaPdf());
            App.DebugLog("=== COMMANDS INICIALIZADOS COM SUCESSO ===");
        }

        #endregion

        #region Command Implementations

        private async Task CriarNovoQuestionario()
        {
            try
            {
                IsLoading = true;
                await Task.Delay(500); // Simular carregamento
                
                MessageBox.Show("Novo questionário criado com sucesso!\n\nEsta funcionalidade será completamente implementada em breve.",
                               "Questionário Criado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar novo questionário: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task CarregarQuestionarioExistente()
        {
            try
            {
                IsLoading = true;
                await Task.Delay(500);
                
                MessageBox.Show("Funcionalidade de carregar questionário existente será implementada em breve.",
                               "Em desenvolvimento", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar questionário: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task GuardarQuestionario()
        {
            App.DebugLog("🔥🔥🔥 MÉTODO GUARDAR QUESTIONÁRIO CHAMADO! 🔥🔥🔥");
            
            try
            {
                IsLoading = true;
                
                App.DebugLog("=== INICIANDO GUARDAR QUESTIONÁRIO ===");
                App.DebugLog($"PacienteId: {PacienteId}");
                
                if (PacienteId <= 0)
                {
                    App.DebugLog("ERRO: PacienteId inválido");
                    MessageBox.Show("Erro: Paciente não identificado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using var context = new BioDeskDbContext(new DbContextOptionsBuilder<BioDeskDbContext>().Options);
                
                App.DebugLog("Contexto de base de dados criado");
                
                // Verificar se já existe um questionário para este paciente
                var questionarioExistente = await context.QuestionariosSaude
                    .FirstOrDefaultAsync(q => q.PacienteId == PacienteId);

                QuestionarioSaude questionario;
                
                if (questionarioExistente != null)
                {
                    App.DebugLog($"Atualizando questionário existente ID: {questionarioExistente.Id}");
                    questionario = questionarioExistente;
                }
                else
                {
                    App.DebugLog("Criando novo questionário");
                    questionario = new QuestionarioSaude { PacienteId = PacienteId };
                    context.QuestionariosSaude.Add(questionario);
                }

                // Mapear os dados do ViewModel para o modelo
                questionario.CondicoesCronicas = DoencaCronica;
                questionario.SintomasAtuais = SintomasAtuais;
                questionario.MedicacaoAtual = MedicacaoAtual;
                questionario.AlergiasAlimentos = AlergiasAlimentares;
                questionario.AlergiasMedicamentos = AlergiasMedicamentos;
                questionario.AlergiasAmbientais = AlergiasAmbientais;
                questionario.AlergiasPlantas = OutrasAlergias;
                questionario.HistoricoCirurgias = HistoricoCirurgico;
                questionario.HistoricoFraturas = Fraturas;
                questionario.DetalhesEstiloVida = $"Atividade Física: {AtividadeFisica}; Tabagismo: {Tabagismo}; Álcool: {ConsumoAlcool}; Hábitos Alimentares: {HabitosAlimentares}";
                questionario.DataUltimaAtualizacao = DateTime.Now;

                // Definir campos boolean baseado se há texto
                questionario.TomaMedicacao = !string.IsNullOrWhiteSpace(MedicacaoAtual);
                questionario.JaFezCirurgias = !string.IsNullOrWhiteSpace(HistoricoCirurgico);
                questionario.JaTeveFraturas = !string.IsNullOrWhiteSpace(Fraturas);
                questionario.TemAlergias = !string.IsNullOrWhiteSpace(AlergiasAlimentares) || 
                                          !string.IsNullOrWhiteSpace(AlergiasMedicamentos) || 
                                          !string.IsNullOrWhiteSpace(AlergiasAmbientais) || 
                                          !string.IsNullOrWhiteSpace(OutrasAlergias);
                questionario.PraticaExercicio = !string.IsNullOrWhiteSpace(AtividadeFisica);
                questionario.Fuma = !string.IsNullOrWhiteSpace(Tabagismo) && !Tabagismo.ToLower().Contains("não");
                questionario.ConsomeAlcool = !string.IsNullOrWhiteSpace(ConsumoAlcool) && !ConsumoAlcool.ToLower().Contains("não");

                App.DebugLog("Dados do questionário mapeados");
                App.DebugLog($"CondicoesCronicas: '{questionario.CondicoesCronicas}'");
                App.DebugLog($"SintomasAtuais: '{questionario.SintomasAtuais}'");
                App.DebugLog($"MedicacaoAtual: '{questionario.MedicacaoAtual}'");
                App.DebugLog($"AtividadeFisica: '{AtividadeFisica}'");
                App.DebugLog($"Tabagismo: '{Tabagismo}'");
                App.DebugLog($"ConsumoAlcool: '{ConsumoAlcool}'");
                App.DebugLog($"HabitosAlimentares: '{HabitosAlimentares}'");
                App.DebugLog($"DetalhesEstiloVida: '{questionario.DetalhesEstiloVida}'");
                App.DebugLog($"TomaMedicacao: {questionario.TomaMedicacao}");
                App.DebugLog($"PraticaExercicio: {questionario.PraticaExercicio}");
                App.DebugLog($"Fuma: {questionario.Fuma}");
                App.DebugLog($"ConsomeAlcool: {questionario.ConsomeAlcool}");

                // Guardar na base de dados
                App.DebugLog("Salvando alterações na base de dados...");
                var linhasAfetadas = await context.SaveChangesAsync();
                App.DebugLog($"Operação concluída! Linhas afetadas: {linhasAfetadas}");

                // Atualizar propriedades de interface
                DataUltimaEdicao = DateTime.Now;
                UltimoAutoSave = DateTime.Now;
                TemAlteracoes = false;

                App.DebugLog("=== QUESTIONÁRIO GUARDADO COM SUCESSO ===");
                MessageBox.Show("Questionário guardado com sucesso!", 
                               "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                App.DebugLog($"ERRO ao guardar questionário: {ex.Message}");
                App.DebugLog($"StackTrace: {ex.StackTrace}");
                MessageBox.Show($"Erro ao guardar questionário: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AssinarComoUtente()
        {
            try
            {
                var resultado = MessageBox.Show(
                    "Pretende assinar este questionário como utente?\n\nEsta ação não pode ser desfeita.",
                    "Confirmar Assinatura",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    await Task.Delay(200);
                    MessageBox.Show("Questionário assinado como utente com sucesso!",
                                   "Assinatura Completa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao assinar questionário: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AssinarComoProfissional()
        {
            try
            {
                var resultado = MessageBox.Show(
                    "Pretende assinar este questionário como profissional?\n\nEsta ação não pode ser desfeita.",
                    "Confirmar Assinatura",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    await Task.Delay(200);
                    MessageBox.Show("Questionário assinado como profissional com sucesso!",
                                   "Assinatura Completa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao assinar questionário: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ImprimirQuestionario()
        {
            try
            {
                MessageBox.Show("Funcionalidade de impressão será implementada em breve.",
                               "Em desenvolvimento", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao imprimir: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ExportarParaPdf()
        {
            try
            {
                await Task.Delay(100);
                MessageBox.Show("Funcionalidade de exportação para PDF será implementada em breve.",
                               "Em desenvolvimento", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar para PDF: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region View Communication

        public void OnWebBrowserLoaded()
        {
            // Chamado quando o WebBrowser termina de carregar
            System.Diagnostics.Debug.WriteLine("WebBrowser carregado com sucesso");
        }

        public void ProcessarDadosFormulario(string dados)
        {
            // Processar dados do formulário
            System.Diagnostics.Debug.WriteLine($"Dados do formulário recebidos: {dados}");
        }

        public void Cleanup()
        {
            // Cleanup quando necessário
        }

        #endregion
    }
}