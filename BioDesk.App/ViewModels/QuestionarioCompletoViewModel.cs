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

        #region Sistemas Corporais - Propriedades Detalhadas

        // 1. Sistema Cardiovascular 🫀
        private bool _cardioHipertensao = false;
        public bool CardioHipertensao
        {
            get => _cardioHipertensao;
            set => SetProperty(ref _cardioHipertensao, value);
        }

        private bool _cardioDiabetesTipo1 = false;
        public bool CardioDiabetesTipo1
        {
            get => _cardioDiabetesTipo1;
            set => SetProperty(ref _cardioDiabetesTipo1, value);
        }

        private bool _cardioDiabetesTipo2 = false;
        public bool CardioDiabetesTipo2
        {
            get => _cardioDiabetesTipo2;
            set => SetProperty(ref _cardioDiabetesTipo2, value);
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

        private bool _cardioOutro = false;
        public bool CardioOutro
        {
            get => _cardioOutro;
            set => SetProperty(ref _cardioOutro, value);
        }

        private string _cardioOutroDescricao = string.Empty;
        public string CardioOutroDescricao
        {
            get => _cardioOutroDescricao;
            set => SetProperty(ref _cardioOutroDescricao, value);
        }

        // 2. Sistema Musculoesquelético 🦴
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

        private bool _musculoLombalgia = false;
        public bool MusculoLombalgia
        {
            get => _musculoLombalgia;
            set => SetProperty(ref _musculoLombalgia, value);
        }

        private bool _musculoCervicalgia = false;
        public bool MusculoCervicalgia
        {
            get => _musculoCervicalgia;
            set => SetProperty(ref _musculoCervicalgia, value);
        }

        private bool _musculoOutro = false;
        public bool MusculoOutro
        {
            get => _musculoOutro;
            set => SetProperty(ref _musculoOutro, value);
        }

        private string _musculoOutroDescricao = string.Empty;
        public string MusculoOutroDescricao
        {
            get => _musculoOutroDescricao;
            set => SetProperty(ref _musculoOutroDescricao, value);
        }

        // 3. Sistema Respiratório 🫁
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

        private bool _respiratorioApneiaSono = false;
        public bool RespiratorioApneiaSono
        {
            get => _respiratorioApneiaSono;
            set => SetProperty(ref _respiratorioApneiaSono, value);
        }

        private bool _respiratorioOutro = false;
        public bool RespiratorioOutro
        {
            get => _respiratorioOutro;
            set => SetProperty(ref _respiratorioOutro, value);
        }

        private string _respiratorioOutroDescricao = string.Empty;
        public string RespiratorioOutroDescricao
        {
            get => _respiratorioOutroDescricao;
            set => SetProperty(ref _respiratorioOutroDescricao, value);
        }

        // 4. Sistema Digestivo 🍃
        private bool _digestivoGastrite = false;
        public bool DigestivoGastrite
        {
            get => _digestivoGastrite;
            set => SetProperty(ref _digestivoGastrite, value);
        }

        private bool _digestivoRefluxo = false;
        public bool DigestivoRefluxo
        {
            get => _digestivoRefluxo;
            set => SetProperty(ref _digestivoRefluxo, value);
        }

        private bool _digestivoSII = false;
        public bool DigestivoSII
        {
            get => _digestivoSII;
            set => SetProperty(ref _digestivoSII, value);
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

        private bool _digestivoColiteUlcerosa = false;
        public bool DigestivoColiteUlcerosa
        {
            get => _digestivoColiteUlcerosa;
            set => SetProperty(ref _digestivoColiteUlcerosa, value);
        }

        private bool _digestivoConstipacao = false;
        public bool DigestivoConstipacao
        {
            get => _digestivoConstipacao;
            set => SetProperty(ref _digestivoConstipacao, value);
        }

        private bool _digestivoOutro = false;
        public bool DigestivoOutro
        {
            get => _digestivoOutro;
            set => SetProperty(ref _digestivoOutro, value);
        }

        private string _digestivoOutroDescricao = string.Empty;
        public string DigestivoOutroDescricao
        {
            get => _digestivoOutroDescricao;
            set => SetProperty(ref _digestivoOutroDescricao, value);
        }

        // 5. Sistema Neurológico 🧠
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

        private bool _neurologicoVertigensTonturas = false;
        public bool NeurologicoVertigensTonturas
        {
            get => _neurologicoVertigensTonturas;
            set => SetProperty(ref _neurologicoVertigensTonturas, value);
        }

        private bool _neurologicoOutro = false;
        public bool NeurologicoOutro
        {
            get => _neurologicoOutro;
            set => SetProperty(ref _neurologicoOutro, value);
        }

        private string _neurologicoOutroDescricao = string.Empty;
        public string NeurologicoOutroDescricao
        {
            get => _neurologicoOutroDescricao;
            set => SetProperty(ref _neurologicoOutroDescricao, value);
        }

        // 6. Sistema Endócrino 🦋
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

        private bool _endocrinoMenopausa = false;
        public bool EndocrinoMenopausa
        {
            get => _endocrinoMenopausa;
            set => SetProperty(ref _endocrinoMenopausa, value);
        }

        private bool _endocrinoOutro = false;
        public bool EndocrinoOutro
        {
            get => _endocrinoOutro;
            set => SetProperty(ref _endocrinoOutro, value);
        }

        private string _endocrinoOutroDescricao = string.Empty;
        public string EndocrinoOutroDescricao
        {
            get => _endocrinoOutroDescricao;
            set => SetProperty(ref _endocrinoOutroDescricao, value);
        }

        // 7. Sistema Renal/Geniturinário 🔄
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

        private bool _renalOutro = false;
        public bool RenalOutro
        {
            get => _renalOutro;
            set => SetProperty(ref _renalOutro, value);
        }

        private string _renalOutroDescricao = string.Empty;
        public string RenalOutroDescricao
        {
            get => _renalOutroDescricao;
            set => SetProperty(ref _renalOutroDescricao, value);
        }

        // 8. Sistema Pele/Dermatológico 🌟
        private bool _peleEczema = false;
        public bool PeleEczema
        {
            get => _peleEczema;
            set => SetProperty(ref _peleEczema, value);
        }

        private bool _pelePsoriase = false;
        public bool PelePsoriase
        {
            get => _pelePsoriase;
            set => SetProperty(ref _pelePsoriase, value);
        }

        private bool _peleDermatite = false;
        public bool PeleDermatite
        {
            get => _peleDermatite;
            set => SetProperty(ref _peleDermatite, value);
        }

        private bool _peleAlergiasCutaneas = false;
        public bool PeleAlergiasCutaneas
        {
            get => _peleAlergiasCutaneas;
            set => SetProperty(ref _peleAlergiasCutaneas, value);
        }

        private bool _peleAcne = false;
        public bool PeleAcne
        {
            get => _peleAcne;
            set => SetProperty(ref _peleAcne, value);
        }

        private bool _peleOutro = false;
        public bool PeleOutro
        {
            get => _peleOutro;
            set => SetProperty(ref _peleOutro, value);
        }

        private string _peleOutroDescricao = string.Empty;
        public string PeleOutroDescricao
        {
            get => _peleOutroDescricao;
            set => SetProperty(ref _peleOutroDescricao, value);
        }

        // 9. Sistema Oftalmológico 👁️
        private bool _oftalmologicoMiopia = false;
        public bool OftalmologicoMiopia
        {
            get => _oftalmologicoMiopia;
            set => SetProperty(ref _oftalmologicoMiopia, value);
        }

        private bool _oftalmologicoHipermetropia = false;
        public bool OftalmologicoHipermetropia
        {
            get => _oftalmologicoHipermetropia;
            set => SetProperty(ref _oftalmologicoHipermetropia, value);
        }

        private bool _oftalmologicoAstigmatismo = false;
        public bool OftalmologicoAstigmatismo
        {
            get => _oftalmologicoAstigmatismo;
            set => SetProperty(ref _oftalmologicoAstigmatismo, value);
        }

        private bool _oftalmologicoGlaucoma = false;
        public bool OftalmologicoGlaucoma
        {
            get => _oftalmologicoGlaucoma;
            set => SetProperty(ref _oftalmologicoGlaucoma, value);
        }

        private bool _oftalmologicoCatarata = false;
        public bool OftalmologicoCatarata
        {
            get => _oftalmologicoCatarata;
            set => SetProperty(ref _oftalmologicoCatarata, value);
        }

        private bool _oftalmologicoConjuntivite = false;
        public bool OftalmologicoConjuntivite
        {
            get => _oftalmologicoConjuntivite;
            set => SetProperty(ref _oftalmologicoConjuntivite, value);
        }

        private bool _oftalmologicoOutro = false;
        public bool OftalmologicoOutro
        {
            get => _oftalmologicoOutro;
            set => SetProperty(ref _oftalmologicoOutro, value);
        }

        private string _oftalmologicoOutroDescricao = string.Empty;
        public string OftalmologicoOutroDescricao
        {
            get => _oftalmologicoOutroDescricao;
            set => SetProperty(ref _oftalmologicoOutroDescricao, value);
        }

        // 10. Sistema Auditivo/Otorrino 👂
        private bool _auditivoPerdasAuditivas = false;
        public bool AuditivoPerdasAuditivas
        {
            get => _auditivoPerdasAuditivas;
            set => SetProperty(ref _auditivoPerdasAuditivas, value);
        }

        private bool _auditivoZumbidoOuvido = false;
        public bool AuditivoZumbidoOuvido
        {
            get => _auditivoZumbidoOuvido;
            set => SetProperty(ref _auditivoZumbidoOuvido, value);
        }

        private bool _auditivoOtitesRecorrentes = false;
        public bool AuditivoOtitesRecorrentes
        {
            get => _auditivoOtitesRecorrentes;
            set => SetProperty(ref _auditivoOtitesRecorrentes, value);
        }

        private bool _auditivoLabirintite = false;
        public bool AuditivoLabirintite
        {
            get => _auditivoLabirintite;
            set => SetProperty(ref _auditivoLabirintite, value);
        }

        private bool _auditivoSinusite = false;
        public bool AuditivoSinusite
        {
            get => _auditivoSinusite;
            set => SetProperty(ref _auditivoSinusite, value);
        }

        private bool _auditivoOutro = false;
        public bool AuditivoOutro
        {
            get => _auditivoOutro;
            set => SetProperty(ref _auditivoOutro, value);
        }

        private string _auditivoOutroDescricao = string.Empty;
        public string AuditivoOutroDescricao
        {
            get => _auditivoOutroDescricao;
            set => SetProperty(ref _auditivoOutroDescricao, value);
        }

        // 11. Sistema Saúde Oral 🦷
        private bool _saudeOralCaries = false;
        public bool SaudeOralCaries
        {
            get => _saudeOralCaries;
            set => SetProperty(ref _saudeOralCaries, value);
        }

        private bool _saudeOralGengivite = false;
        public bool SaudeOralGengivite
        {
            get => _saudeOralGengivite;
            set => SetProperty(ref _saudeOralGengivite, value);
        }

        private bool _saudeOralPeriodontite = false;
        public bool SaudeOralPeriodontite
        {
            get => _saudeOralPeriodontite;
            set => SetProperty(ref _saudeOralPeriodontite, value);
        }

        private bool _saudeOralBruxismo = false;
        public bool SaudeOralBruxismo
        {
            get => _saudeOralBruxismo;
            set => SetProperty(ref _saudeOralBruxismo, value);
        }

        private bool _saudeOralSensibilidadeDentaria = false;
        public bool SaudeOralSensibilidadeDentaria
        {
            get => _saudeOralSensibilidadeDentaria;
            set => SetProperty(ref _saudeOralSensibilidadeDentaria, value);
        }

        private bool _saudeOralMauHalito = false;
        public bool SaudeOralMauHalito
        {
            get => _saudeOralMauHalito;
            set => SetProperty(ref _saudeOralMauHalito, value);
        }

        private bool _saudeOralOutro = false;
        public bool SaudeOralOutro
        {
            get => _saudeOralOutro;
            set => SetProperty(ref _saudeOralOutro, value);
        }

        private string _saudeOralOutroDescricao = string.Empty;
        public string SaudeOralOutroDescricao
        {
            get => _saudeOralOutroDescricao;
            set => SetProperty(ref _saudeOralOutroDescricao, value);
        }

        // 12. Sistema Ginecológico/Reprodutivo ♀️
        private bool _ginecologicoEndometriose = false;
        public bool GinecologicoEndometriose
        {
            get => _ginecologicoEndometriose;
            set => SetProperty(ref _ginecologicoEndometriose, value);
        }

        private bool _ginecologicoSOP = false;
        public bool GinecologicoSOP
        {
            get => _ginecologicoSOP;
            set => SetProperty(ref _ginecologicoSOP, value);
        }

        private bool _ginecologicoMiomas = false;
        public bool GinecologicoMiomas
        {
            get => _ginecologicoMiomas;
            set => SetProperty(ref _ginecologicoMiomas, value);
        }

        private bool _ginecologicoCiclosIrregulares = false;
        public bool GinecologicoCiclosIrregulares
        {
            get => _ginecologicoCiclosIrregulares;
            set => SetProperty(ref _ginecologicoCiclosIrregulares, value);
        }

        private bool _ginecologicoInfecaoUrinaria = false;
        public bool GinecologicoInfecaoUrinaria
        {
            get => _ginecologicoInfecaoUrinaria;
            set => SetProperty(ref _ginecologicoInfecaoUrinaria, value);
        }

        private bool _ginecologicoOutro = false;
        public bool GinecologicoOutro
        {
            get => _ginecologicoOutro;
            set => SetProperty(ref _ginecologicoOutro, value);
        }

        private string _ginecologicoOutroDescricao = string.Empty;
        public string GinecologicoOutroDescricao
        {
            get => _ginecologicoOutroDescricao;
            set => SetProperty(ref _ginecologicoOutroDescricao, value);
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

                // Mapear sistemas corporais detalhados
                // 1. Sistema Cardiovascular
                questionario.CardioHipertensao = CardioHipertensao;
                questionario.CardioDiabetesTipo1 = CardioDiabetesTipo1;
                questionario.CardioDiabetesTipo2 = CardioDiabetesTipo2;
                questionario.CardioArritmia = CardioArritmia;
                questionario.CardioColesterolAlto = CardioColesterolAlto;
                questionario.CardioInsuficienciaCardiaca = CardioInsuficienciaCardiaca;
                questionario.CardioAngina = CardioAngina;
                questionario.CardioOutro = CardioOutro;
                questionario.CardioOutroDescricao = CardioOutroDescricao;

                // 2. Sistema Musculoesquelético
                questionario.MusculoArtrose = MusculoArtrose;
                questionario.MusculoTendinite = MusculoTendinite;
                questionario.MusculoHerniaDiscal = MusculoHerniaDiscal;
                questionario.MusculoFibromialgia = MusculoFibromialgia;
                questionario.MusculoArtrite = MusculoArtrite;
                questionario.MusculoBursite = MusculoBursite;
                questionario.MusculoLombalgia = MusculoLombalgia;
                questionario.MusculoCervicalgia = MusculoCervicalgia;
                questionario.MusculoOutro = MusculoOutro;
                questionario.MusculoOutroDescricao = MusculoOutroDescricao;

                // 3. Sistema Respiratório
                questionario.RespiratorioAsma = RespiratorioAsma;
                questionario.RespiratorioSinusiteCronica = RespiratorioSinusiteCronica;
                questionario.RespiratorioBronquite = RespiratorioBronquite;
                questionario.RespiratorioRiniteAlergica = RespiratorioRiniteAlergica;
                questionario.RespiratorioApneiaSono = RespiratorioApneiaSono;
                questionario.RespiratorioOutro = RespiratorioOutro;
                questionario.RespiratorioOutroDescricao = RespiratorioOutroDescricao;

                // 4. Sistema Digestivo
                questionario.DigestivoGastrite = DigestivoGastrite;
                questionario.DigestivoRefluxo = DigestivoRefluxo;
                questionario.DigestivoSII = DigestivoSII;
                questionario.DigestivoIntoleranciaLactose = DigestivoIntoleranciaLactose;
                questionario.DigestivoDoencaCrohn = DigestivoDoencaCrohn;
                questionario.DigestivoColiteUlcerosa = DigestivoColiteUlcerosa;
                questionario.DigestivoConstipacao = DigestivoConstipacao;
                questionario.DigestivoOutro = DigestivoOutro;
                questionario.DigestivoOutroDescricao = DigestivoOutroDescricao;

                // 5. Sistema Neurológico
                questionario.NeurologicoEnxaquecas = NeurologicoEnxaquecas;
                questionario.NeurologicoAnsiedade = NeurologicoAnsiedade;
                questionario.NeurologicoDepressao = NeurologicoDepressao;
                questionario.NeurologicoInsonia = NeurologicoInsonia;
                questionario.NeurologicoEpilepsia = NeurologicoEpilepsia;
                questionario.NeurologicoVertigensTonturas = NeurologicoVertigensTonturas;
                questionario.NeurologicoOutro = NeurologicoOutro;
                questionario.NeurologicoOutroDescricao = NeurologicoOutroDescricao;

                // 6. Sistema Endócrino
                questionario.EndocrinoHipotiroidismo = EndocrinoHipotiroidismo;
                questionario.EndocrinoHipertiroidismo = EndocrinoHipertiroidismo;
                questionario.EndocrinoPCOS = EndocrinoPCOS;
                questionario.EndocrinoResistenciaInsulina = EndocrinoResistenciaInsulina;
                questionario.EndocrinoMenopausa = EndocrinoMenopausa;
                questionario.EndocrinoOutro = EndocrinoOutro;
                questionario.EndocrinoOutroDescricao = EndocrinoOutroDescricao;

                // 7. Sistema Renal/Geniturinário
                questionario.RenalInfecaoUrinaria = RenalInfecaoUrinaria;
                questionario.RenalCalculosRenais = RenalCalculosRenais;
                questionario.RenalCistite = RenalCistite;
                questionario.RenalIncontinencia = RenalIncontinencia;
                questionario.RenalOutro = RenalOutro;
                questionario.RenalOutroDescricao = RenalOutroDescricao;

                // 8. Sistema Pele/Dermatológico
                questionario.PeleEczema = PeleEczema;
                questionario.PelePsoriase = PelePsoriase;
                questionario.PeleDermatite = PeleDermatite;
                questionario.PeleAlergiasCutaneas = PeleAlergiasCutaneas;
                questionario.PeleAcne = PeleAcne;
                questionario.PeleOutro = PeleOutro;
                questionario.PeleOutroDescricao = PeleOutroDescricao;

                // 9. Sistema Oftalmológico
                questionario.OftalmologicoMiopia = OftalmologicoMiopia;
                questionario.OftalmologicoHipermetropia = OftalmologicoHipermetropia;
                questionario.OftalmologicoAstigmatismo = OftalmologicoAstigmatismo;
                questionario.OftalmologicoGlaucoma = OftalmologicoGlaucoma;
                questionario.OftalmologicoCatarata = OftalmologicoCatarata;
                questionario.OftalmologicoConjuntivite = OftalmologicoConjuntivite;
                questionario.OftalmologicoOutro = OftalmologicoOutro;
                questionario.OftalmologicoOutroDescricao = OftalmologicoOutroDescricao;

                // 10. Sistema Auditivo/Otorrino
                questionario.AuditivoPerdasAuditivas = AuditivoPerdasAuditivas;
                questionario.AuditivoZumbidoOuvido = AuditivoZumbidoOuvido;
                questionario.AuditivoOtitesRecorrentes = AuditivoOtitesRecorrentes;
                questionario.AuditivoLabirintite = AuditivoLabirintite;
                questionario.AuditivoSinusite = AuditivoSinusite;
                questionario.AuditivoOutro = AuditivoOutro;
                questionario.AuditivoOutroDescricao = AuditivoOutroDescricao;

                // 11. Sistema Saúde Oral
                questionario.SaudeOralCaries = SaudeOralCaries;
                questionario.SaudeOralGengivite = SaudeOralGengivite;
                questionario.SaudeOralPeriodontite = SaudeOralPeriodontite;
                questionario.SaudeOralBruxismo = SaudeOralBruxismo;
                questionario.SaudeOralSensibilidadeDentaria = SaudeOralSensibilidadeDentaria;
                questionario.SaudeOralMauHalito = SaudeOralMauHalito;
                questionario.SaudeOralOutro = SaudeOralOutro;
                questionario.SaudeOralOutroDescricao = SaudeOralOutroDescricao;

                // 12. Sistema Ginecológico/Reprodutivo
                questionario.GinecologicoEndometriose = GinecologicoEndometriose;
                questionario.GinecologicoSOP = GinecologicoSOP;
                questionario.GinecologicoMiomas = GinecologicoMiomas;
                questionario.GinecologicoCiclosIrregulares = GinecologicoCiclosIrregulares;
                questionario.GinecologicoInfecaoUrinaria = GinecologicoInfecaoUrinaria;
                questionario.GinecologicoOutro = GinecologicoOutro;
                questionario.GinecologicoOutroDescricao = GinecologicoOutroDescricao;

                // Atualizar campos genéricos baseados nas seleções específicas
                questionario.ProblemasCardiovasculares = CardioHipertensao || CardioDiabetesTipo1 || CardioDiabetesTipo2 || 
                                                        CardioArritmia || CardioColesterolAlto || CardioInsuficienciaCardiaca || 
                                                        CardioAngina || CardioOutro;

                questionario.ProblemasMusculoesqueleticos = MusculoArtrose || MusculoTendinite || MusculoHerniaDiscal || 
                                                           MusculoFibromialgia || MusculoArtrite || MusculoBursite || 
                                                           MusculoLombalgia || MusculoCervicalgia || MusculoOutro;

                questionario.ProblemasRespiratorios = RespiratorioAsma || RespiratorioSinusiteCronica || RespiratorioBronquite || 
                                                     RespiratorioRiniteAlergica || RespiratorioApneiaSono || RespiratorioOutro;

                questionario.ProblemasDigestivos = DigestivoGastrite || DigestivoRefluxo || DigestivoSII || 
                                                  DigestivoIntoleranciaLactose || DigestivoDoencaCrohn || 
                                                  DigestivoColiteUlcerosa || DigestivoConstipacao || DigestivoOutro;

                questionario.ProblemasNeurologicos = NeurologicoEnxaquecas || NeurologicoAnsiedade || NeurologicoDepressao || 
                                                    NeurologicoInsonia || NeurologicoEpilepsia || NeurologicoVertigensTonturas || 
                                                    NeurologicoOutro;

                questionario.ProblemasEndocrinos = EndocrinoHipotiroidismo || EndocrinoHipertiroidismo || EndocrinoPCOS || 
                                                  EndocrinoResistenciaInsulina || EndocrinoMenopausa || EndocrinoOutro;

                questionario.ProblemasRenais = RenalInfecaoUrinaria || RenalCalculosRenais || RenalCistite || 
                                             RenalIncontinencia || RenalOutro;

                questionario.ProblemasPele = PeleEczema || PelePsoriase || PeleDermatite || PeleAlergiasCutaneas || 
                                           PeleAcne || PeleOutro;

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