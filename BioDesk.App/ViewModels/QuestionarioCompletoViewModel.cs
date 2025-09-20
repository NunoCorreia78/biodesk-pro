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

        // Propriedades para condições específicas por sistema - Cardiovasculares
        private bool _cardioHipertensao = false;
        public bool CardioHipertensao
        {
            get => _cardioHipertensao;
            set => SetProperty(ref _cardioHipertensao, value);
        }

        private bool _cardioArritmia = false;
        public bool CardioArritmia
        {
            get => _cardioArritmia;
            set => SetProperty(ref _cardioArritmia, value);
        }

        private bool _cardioColesterolAlto = false;
        public bool CardioColesterolAlto
        {
            get => _cardioColesterolAlto;
            set => SetProperty(ref _cardioColesterolAlto, value);
        }

        private bool _cardioInsuficienciaCardiaca = false;
        public bool CardioInsuficienciaCardiaca
        {
            get => _cardioInsuficienciaCardiaca;
            set => SetProperty(ref _cardioInsuficienciaCardiaca, value);
        }

        private bool _cardioAngina = false;
        public bool CardioAngina
        {
            get => _cardioAngina;
            set => SetProperty(ref _cardioAngina, value);
        }

        private bool _cardioInfartoMiocardio = false;
        public bool CardioInfartoMiocardio
        {
            get => _cardioInfartoMiocardio;
            set => SetProperty(ref _cardioInfartoMiocardio, value);
        }

        // Musculoesqueléticas
        private bool _musculoArtrose = false;
        public bool MusculoArtrose
        {
            get => _musculoArtrose;
            set => SetProperty(ref _musculoArtrose, value);
        }

        private bool _musculoTendinite = false;
        public bool MusculoTendinite
        {
            get => _musculoTendinite;
            set => SetProperty(ref _musculoTendinite, value);
        }

        private bool _musculoHerniaDiscal = false;
        public bool MusculoHerniaDiscal
        {
            get => _musculoHerniaDiscal;
            set => SetProperty(ref _musculoHerniaDiscal, value);
        }

        private bool _musculoFibromialgia = false;
        public bool MusculoFibromialgia
        {
            get => _musculoFibromialgia;
            set => SetProperty(ref _musculoFibromialgia, value);
        }

        private bool _musculoArtrite = false;
        public bool MusculoArtrite
        {
            get => _musculoArtrite;
            set => SetProperty(ref _musculoArtrite, value);
        }

        private bool _musculoBursite = false;
        public bool MusculoBursite
        {
            get => _musculoBursite;
            set => SetProperty(ref _musculoBursite, value);
        }

        private bool _musculoLombalgias = false;
        public bool MusculoLombalgias
        {
            get => _musculoLombalgias;
            set => SetProperty(ref _musculoLombalgias, value);
        }

        // Respiratórias
        private bool _respiratorioAsma = false;
        public bool RespiratorioAsma
        {
            get => _respiratorioAsma;
            set => SetProperty(ref _respiratorioAsma, value);
        }

        private bool _respiratorioSinusiteCronica = false;
        public bool RespiratorioSinusiteCronica
        {
            get => _respiratorioSinusiteCronica;
            set => SetProperty(ref _respiratorioSinusiteCronica, value);
        }

        private bool _respiratorioBronquite = false;
        public bool RespiratorioBronquite
        {
            get => _respiratorioBronquite;
            set => SetProperty(ref _respiratorioBronquite, value);
        }

        private bool _respiratorioRiniteAlergica = false;
        public bool RespiratorioRiniteAlergica
        {
            get => _respiratorioRiniteAlergica;
            set => SetProperty(ref _respiratorioRiniteAlergica, value);
        }

        private bool _respiratorioApneiaDoSono = false;
        public bool RespiratorioApneiaDoSono
        {
            get => _respiratorioApneiaDoSono;
            set => SetProperty(ref _respiratorioApneiaDoSono, value);
        }

        // Digestivas
        private bool _digestivoGastrite = false;
        public bool DigestivoGastrite
        {
            get => _digestivoGastrite;
            set => SetProperty(ref _digestivoGastrite, value);
        }

        private bool _digestivoSindromeIntestinoIrritavel = false;
        public bool DigestivoSindromeIntestinoIrritavel
        {
            get => _digestivoSindromeIntestinoIrritavel;
            set => SetProperty(ref _digestivoSindromeIntestinoIrritavel, value);
        }

        private bool _digestivoRefluxoGastroesofagico = false;
        public bool DigestivoRefluxoGastroesofagico
        {
            get => _digestivoRefluxoGastroesofagico;
            set => SetProperty(ref _digestivoRefluxoGastroesofagico, value);
        }

        private bool _digestivoIntoleranciaLactose = false;
        public bool DigestivoIntoleranciaLactose
        {
            get => _digestivoIntoleranciaLactose;
            set => SetProperty(ref _digestivoIntoleranciaLactose, value);
        }

        private bool _digestivoDoencaCrohn = false;
        public bool DigestivoDoencaCrohn
        {
            get => _digestivoDoencaCrohn;
            set => SetProperty(ref _digestivoDoencaCrohn, value);
        }

        private bool _digestivoUlceraPeptica = false;
        public bool DigestivoUlceraPeptica
        {
            get => _digestivoUlceraPeptica;
            set => SetProperty(ref _digestivoUlceraPeptica, value);
        }

        // Neurológicas
        private bool _neurologicoEnxaquecas = false;
        public bool NeurologicoEnxaquecas
        {
            get => _neurologicoEnxaquecas;
            set => SetProperty(ref _neurologicoEnxaquecas, value);
        }

        private bool _neurologicoAnsiedade = false;
        public bool NeurologicoAnsiedade
        {
            get => _neurologicoAnsiedade;
            set => SetProperty(ref _neurologicoAnsiedade, value);
        }

        private bool _neurologicoDepressao = false;
        public bool NeurologicoDepressao
        {
            get => _neurologicoDepressao;
            set => SetProperty(ref _neurologicoDepressao, value);
        }

        private bool _neurologicoInsonia = false;
        public bool NeurologicoInsonia
        {
            get => _neurologicoInsonia;
            set => SetProperty(ref _neurologicoInsonia, value);
        }

        private bool _neurologicoEpilepsia = false;
        public bool NeurologicoEpilepsia
        {
            get => _neurologicoEpilepsia;
            set => SetProperty(ref _neurologicoEpilepsia, value);
        }

        private bool _neurologicoTranstornoBipolar = false;
        public bool NeurologicoTranstornoBipolar
        {
            get => _neurologicoTranstornoBipolar;
            set => SetProperty(ref _neurologicoTranstornoBipolar, value);
        }

        // Endócrinas
        private bool _endocrinoDiabetesTipo1 = false;
        public bool EndocrinoDiabetesTipo1
        {
            get => _endocrinoDiabetesTipo1;
            set => SetProperty(ref _endocrinoDiabetesTipo1, value);
        }

        private bool _endocrinoDiabetesTipo2 = false;
        public bool EndocrinoDiabetesTipo2
        {
            get => _endocrinoDiabetesTipo2;
            set => SetProperty(ref _endocrinoDiabetesTipo2, value);
        }

        private bool _endocrinoHipotiroidismo = false;
        public bool EndocrinoHipotiroidismo
        {
            get => _endocrinoHipotiroidismo;
            set => SetProperty(ref _endocrinoHipotiroidismo, value);
        }

        private bool _endocrinoHipertiroidismo = false;
        public bool EndocrinoHipertiroidismo
        {
            get => _endocrinoHipertiroidismo;
            set => SetProperty(ref _endocrinoHipertiroidismo, value);
        }

        private bool _endocrinoPCOS = false;
        public bool EndocrinoPCOS
        {
            get => _endocrinoPCOS;
            set => SetProperty(ref _endocrinoPCOS, value);
        }

        private bool _endocrinoResistenciaInsulina = false;
        public bool EndocrinoResistenciaInsulina
        {
            get => _endocrinoResistenciaInsulina;
            set => SetProperty(ref _endocrinoResistenciaInsulina, value);
        }

        // Renais/Geniturinários
        private bool _renalInfecaoUrinaria = false;
        public bool RenalInfecaoUrinaria
        {
            get => _renalInfecaoUrinaria;
            set => SetProperty(ref _renalInfecaoUrinaria, value);
        }

        private bool _renalCalculosRenais = false;
        public bool RenalCalculosRenais
        {
            get => _renalCalculosRenais;
            set => SetProperty(ref _renalCalculosRenais, value);
        }

        private bool _renalCistite = false;
        public bool RenalCistite
        {
            get => _renalCistite;
            set => SetProperty(ref _renalCistite, value);
        }

        private bool _renalIncontinencia = false;
        public bool RenalIncontinencia
        {
            get => _renalIncontinencia;
            set => SetProperty(ref _renalIncontinencia, value);
        }

        private bool _renalInsuficienciaRenal = false;
        public bool RenalInsuficienciaRenal
        {
            get => _renalInsuficienciaRenal;
            set => SetProperty(ref _renalInsuficienciaRenal, value);
        }

        #endregion

        #region Methods

        public void SetPaciente(int pacienteId, string nomePaciente)
        {
            PacienteId = pacienteId;
            NomePaciente = nomePaciente;
            App.DebugLog($"QuestionarioCompletoViewModel: Paciente definido - ID: {pacienteId}, Nome: {nomePaciente}");
            
            // Carregar questionário existente, se houver
            _ = CarregarQuestionarioExistente();
        }

        private async Task CarregarQuestionarioDados()
        {
            try
            {
                if (PacienteId <= 0) return;

                using var context = new BioDeskDbContext(new DbContextOptionsBuilder<BioDeskDbContext>().Options);
                
                var questionario = await context.QuestionariosSaude
                    .FirstOrDefaultAsync(q => q.PacienteId == PacienteId);

                if (questionario != null)
                {
                    // Carregar dados gerais
                    DoencaCronica = questionario.CondicoesCronicas ?? string.Empty;
                    SintomasAtuais = questionario.SintomasAtuais ?? string.Empty;
                    MedicacaoAtual = questionario.MedicacaoAtual ?? string.Empty;
                    AlergiasAlimentares = questionario.AlergiasAlimentos ?? string.Empty;
                    AlergiasMedicamentos = questionario.AlergiasMedicamentos ?? string.Empty;
                    AlergiasAmbientais = questionario.AlergiasAmbientais ?? string.Empty;
                    OutrasAlergias = questionario.AlergiasPlantas ?? string.Empty;
                    HistoricoCirurgico = questionario.HistoricoCirurgias ?? string.Empty;
                    Fraturas = questionario.HistoricoFraturas ?? string.Empty;

                    // Carregar condições específicas - Cardiovasculares
                    CardioHipertensao = questionario.CardioHipertensao;
                    CardioArritmia = questionario.CardioArritmia;
                    CardioColesterolAlto = questionario.CardioColesterolAlto;
                    CardioInsuficienciaCardiaca = questionario.CardioInsuficienciaCardiaca;
                    CardioAngina = questionario.CardioAngina;
                    CardioInfartoMiocardio = questionario.CardioInfartoMiocardio;

                    // Musculoesqueléticas
                    MusculoArtrose = questionario.MusculoArtrose;
                    MusculoTendinite = questionario.MusculoTendinite;
                    MusculoHerniaDiscal = questionario.MusculoHerniaDiscal;
                    MusculoFibromialgia = questionario.MusculoFibromialgia;
                    MusculoArtrite = questionario.MusculoArtrite;
                    MusculoBursite = questionario.MusculoBursite;
                    MusculoLombalgias = questionario.MusculoLombalgias;

                    // Respiratórias
                    RespiratorioAsma = questionario.RespiratorioAsma;
                    RespiratorioSinusiteCronica = questionario.RespiratorioSinusiteCronica;
                    RespiratorioBronquite = questionario.RespiratorioBronquite;
                    RespiratorioRiniteAlergica = questionario.RespiratorioRiniteAlergica;
                    RespiratorioApneiaDoSono = questionario.RespiratorioApneiaDoSono;

                    // Digestivas
                    DigestivoGastrite = questionario.DigestivoGastrite;
                    DigestivoSindromeIntestinoIrritavel = questionario.DigestivoSindromeIntestinoIrritavel;
                    DigestivoRefluxoGastroesofagico = questionario.DigestivoRefluxoGastroesofagico;
                    DigestivoIntoleranciaLactose = questionario.DigestivoIntoleranciaLactose;
                    DigestivoDoencaCrohn = questionario.DigestivoDoencaCrohn;
                    DigestivoUlceraPeptica = questionario.DigestivoUlceraPeptica;

                    // Neurológicas
                    NeurologicoEnxaquecas = questionario.NeurologicoEnxaquecas;
                    NeurologicoAnsiedade = questionario.NeurologicoAnsiedade;
                    NeurologicoDepressao = questionario.NeurologicoDepressao;
                    NeurologicoInsonia = questionario.NeurologicoInsonia;
                    NeurologicoEpilepsia = questionario.NeurologicoEpilepsia;
                    NeurologicoTranstornoBipolar = questionario.NeurologicoTranstornoBipolar;

                    // Endócrinas
                    EndocrinoDiabetesTipo1 = questionario.EndocrinoDiabetesTipo1;
                    EndocrinoDiabetesTipo2 = questionario.EndocrinoDiabetesTipo2;
                    EndocrinoHipotiroidismo = questionario.EndocrinoHipotiroidismo;
                    EndocrinoHipertiroidismo = questionario.EndocrinoHipertiroidismo;
                    EndocrinoPCOS = questionario.EndocrinoPCOS;
                    EndocrinoResistenciaInsulina = questionario.EndocrinoResistenciaInsulina;

                    // Renais/Geniturinários
                    RenalInfecaoUrinaria = questionario.RenalInfecaoUrinaria;
                    RenalCalculosRenais = questionario.RenalCalculosRenais;
                    RenalCistite = questionario.RenalCistite;
                    RenalIncontinencia = questionario.RenalIncontinencia;
                    RenalInsuficienciaRenal = questionario.RenalInsuficienciaRenal;

                    DataPreenchimento = questionario.DataPreenchimento;
                    DataUltimaEdicao = questionario.DataUltimaAtualizacao ?? questionario.DataPreenchimento;
                }
            }
            catch (Exception ex)
            {
                App.DebugLog($"Erro ao carregar dados do questionário: {ex.Message}");
            }
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
                await CarregarQuestionarioDados();
                
                if (!string.IsNullOrEmpty(DoencaCronica) || CardioHipertensao || MusculoArtrose || 
                    RespiratorioAsma || DigestivoGastrite || NeurologicoEnxaquecas || 
                    EndocrinoDiabetesTipo1 || EndocrinoDiabetesTipo2 || RenalInfecaoUrinaria)
                {
                    MessageBox.Show("Questionário carregado com sucesso!", 
                                   "Dados Carregados", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum questionário encontrado. Pode criar um novo.", 
                                   "Novo Questionário", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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

                // Mapear condições específicas por sistema
                // Cardiovasculares
                questionario.CardioHipertensao = CardioHipertensao;
                questionario.CardioArritmia = CardioArritmia;
                questionario.CardioColesterolAlto = CardioColesterolAlto;
                questionario.CardioInsuficienciaCardiaca = CardioInsuficienciaCardiaca;
                questionario.CardioAngina = CardioAngina;
                questionario.CardioInfartoMiocardio = CardioInfartoMiocardio;

                // Musculoesqueléticas
                questionario.MusculoArtrose = MusculoArtrose;
                questionario.MusculoTendinite = MusculoTendinite;
                questionario.MusculoHerniaDiscal = MusculoHerniaDiscal;
                questionario.MusculoFibromialgia = MusculoFibromialgia;
                questionario.MusculoArtrite = MusculoArtrite;
                questionario.MusculoBursite = MusculoBursite;
                questionario.MusculoLombalgias = MusculoLombalgias;

                // Respiratórias
                questionario.RespiratorioAsma = RespiratorioAsma;
                questionario.RespiratorioSinusiteCronica = RespiratorioSinusiteCronica;
                questionario.RespiratorioBronquite = RespiratorioBronquite;
                questionario.RespiratorioRiniteAlergica = RespiratorioRiniteAlergica;
                questionario.RespiratorioApneiaDoSono = RespiratorioApneiaDoSono;

                // Digestivas
                questionario.DigestivoGastrite = DigestivoGastrite;
                questionario.DigestivoSindromeIntestinoIrritavel = DigestivoSindromeIntestinoIrritavel;
                questionario.DigestivoRefluxoGastroesofagico = DigestivoRefluxoGastroesofagico;
                questionario.DigestivoIntoleranciaLactose = DigestivoIntoleranciaLactose;
                questionario.DigestivoDoencaCrohn = DigestivoDoencaCrohn;
                questionario.DigestivoUlceraPeptica = DigestivoUlceraPeptica;

                // Neurológicas
                questionario.NeurologicoEnxaquecas = NeurologicoEnxaquecas;
                questionario.NeurologicoAnsiedade = NeurologicoAnsiedade;
                questionario.NeurologicoDepressao = NeurologicoDepressao;
                questionario.NeurologicoInsonia = NeurologicoInsonia;
                questionario.NeurologicoEpilepsia = NeurologicoEpilepsia;
                questionario.NeurologicoTranstornoBipolar = NeurologicoTranstornoBipolar;

                // Endócrinas
                questionario.EndocrinoDiabetesTipo1 = EndocrinoDiabetesTipo1;
                questionario.EndocrinoDiabetesTipo2 = EndocrinoDiabetesTipo2;
                questionario.EndocrinoHipotiroidismo = EndocrinoHipotiroidismo;
                questionario.EndocrinoHipertiroidismo = EndocrinoHipertiroidismo;
                questionario.EndocrinoPCOS = EndocrinoPCOS;
                questionario.EndocrinoResistenciaInsulina = EndocrinoResistenciaInsulina;

                // Renais/Geniturinários
                questionario.RenalInfecaoUrinaria = RenalInfecaoUrinaria;
                questionario.RenalCalculosRenais = RenalCalculosRenais;
                questionario.RenalCistite = RenalCistite;
                questionario.RenalIncontinencia = RenalIncontinencia;
                questionario.RenalInsuficienciaRenal = RenalInsuficienciaRenal;

                // Atualizar campos genéricos baseado nas condições específicas
                questionario.ProblemasCardiovasculares = CardioHipertensao || CardioArritmia || CardioColesterolAlto || 
                                                        CardioInsuficienciaCardiaca || CardioAngina || CardioInfartoMiocardio;
                questionario.ProblemasMusculoesqueleticos = MusculoArtrose || MusculoTendinite || MusculoHerniaDiscal || 
                                                          MusculoFibromialgia || MusculoArtrite || MusculoBursite || MusculoLombalgias;
                questionario.ProblemasRespiratorios = RespiratorioAsma || RespiratorioSinusiteCronica || RespiratorioBronquite || 
                                                    RespiratorioRiniteAlergica || RespiratorioApneiaDoSono;
                questionario.ProblemasDigestivos = DigestivoGastrite || DigestivoSindromeIntestinoIrritavel || DigestivoRefluxoGastroesofagico || 
                                                  DigestivoIntoleranciaLactose || DigestivoDoencaCrohn || DigestivoUlceraPeptica;
                questionario.ProblemasNeurologicos = NeurologicoEnxaquecas || NeurologicoAnsiedade || NeurologicoDepressao || 
                                                    NeurologicoInsonia || NeurologicoEpilepsia || NeurologicoTranstornoBipolar;
                questionario.ProblemasEndocrinos = EndocrinoDiabetesTipo1 || EndocrinoDiabetesTipo2 || EndocrinoHipotiroidismo || 
                                                  EndocrinoHipertiroidismo || EndocrinoPCOS || EndocrinoResistenciaInsulina;
                questionario.ProblemasRenais = RenalInfecaoUrinaria || RenalCalculosRenais || RenalCistite || 
                                             RenalIncontinencia || RenalInsuficienciaRenal;

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