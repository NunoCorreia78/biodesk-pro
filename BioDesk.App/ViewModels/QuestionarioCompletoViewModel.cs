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
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BioDesk.App.ViewModels
{
    public class QuestionarioCompletoViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private bool _isLoading;
        private int _pacienteId;

        public QuestionarioCompletoViewModel(INavigationService navigationService, IServiceProvider serviceProvider)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("QuestionarioCompletoViewModel: Construtor iniciado");
                _navigationService = navigationService;
                _serviceProvider = serviceProvider;
                System.Diagnostics.Debug.WriteLine("QuestionarioCompletoViewModel: NavigationService e ServiceProvider atribuídos");
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

        private string _doencaCronicaDetalhes = string.Empty;
        public string DoencaCronicaDetalhes
        {
            get => _doencaCronicaDetalhes;
            set => SetProperty(ref _doencaCronicaDetalhes, value);
        }

        // Doenças crónicas específicas (checkboxes)
        private bool _doencaHipertensao;
        public bool DoencaHipertensao
        {
            get => _doencaHipertensao;
            set => SetProperty(ref _doencaHipertensao, value);
        }

        private bool _doencaCardiopatia;
        public bool DoencaCardiopatia
        {
            get => _doencaCardiopatia;
            set => SetProperty(ref _doencaCardiopatia, value);
        }

        private bool _doencaDiabetesTipo1;
        public bool DoencaDiabetesTipo1
        {
            get => _doencaDiabetesTipo1;
            set => SetProperty(ref _doencaDiabetesTipo1, value);
        }

        private bool _doencaDiabetesTipo2;
        public bool DoencaDiabetesTipo2
        {
            get => _doencaDiabetesTipo2;
            set => SetProperty(ref _doencaDiabetesTipo2, value);
        }

        private bool _doencaTiroideia;
        public bool DoencaTiroideia
        {
            get => _doencaTiroideia;
            set => SetProperty(ref _doencaTiroideia, value);
        }

        private bool _doencaDislipidemia;
        public bool DoencaDislipidemia
        {
            get => _doencaDislipidemia;
            set => SetProperty(ref _doencaDislipidemia, value);
        }

        private bool _doencaRenal;
        public bool DoencaRenal
        {
            get => _doencaRenal;
            set => SetProperty(ref _doencaRenal, value);
        }

        private bool _doencaHepatica;
        public bool DoencaHepatica
        {
            get => _doencaHepatica;
            set => SetProperty(ref _doencaHepatica, value);
        }

        private bool _doencaAutoimune;
        public bool DoencaAutoimune
        {
            get => _doencaAutoimune;
            set => SetProperty(ref _doencaAutoimune, value);
        }

        private bool _doencaOsteoporose;
        public bool DoencaOsteoporose
        {
            get => _doencaOsteoporose;
            set => SetProperty(ref _doencaOsteoporose, value);
        }

        private bool _doencaAsmaDPOC;
        public bool DoencaAsmaDPOC
        {
            get => _doencaAsmaDPOC;
            set => SetProperty(ref _doencaAsmaDPOC, value);
        }

        private bool _doencaOncologia;
        public bool DoencaOncologia
        {
            get => _doencaOncologia;
            set => SetProperty(ref _doencaOncologia, value);
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

        private string _alergiasAlimentaresDetalhes = string.Empty;
        public string AlergiasAlimentaresDetalhes
        {
            get => _alergiasAlimentaresDetalhes;
            set => SetProperty(ref _alergiasAlimentaresDetalhes, value);
        }

        // Intolerâncias alimentares específicas (checkboxes)
        private bool _intoleranciaGluten;
        public bool IntoleranciaGluten
        {
            get => _intoleranciaGluten;
            set => SetProperty(ref _intoleranciaGluten, value);
        }

        private bool _intoleranciaLactose;
        public bool IntoleranciaLactose
        {
            get => _intoleranciaLactose;
            set => SetProperty(ref _intoleranciaLactose, value);
        }

        private bool _intoleranciaProteinaLeite;
        public bool IntoleranciaProteinaLeite
        {
            get => _intoleranciaProteinaLeite;
            set => SetProperty(ref _intoleranciaProteinaLeite, value);
        }

        private bool _intoleranciaOvos;
        public bool IntoleranciaOvos
        {
            get => _intoleranciaOvos;
            set => SetProperty(ref _intoleranciaOvos, value);
        }

        private bool _intoleranciaMarisco;
        public bool IntoleranciaMarisco
        {
            get => _intoleranciaMarisco;
            set => SetProperty(ref _intoleranciaMarisco, value);
        }

        private bool _intoleranciaFrutosSecos;
        public bool IntoleranciaFrutosSecos
        {
            get => _intoleranciaFrutosSecos;
            set => SetProperty(ref _intoleranciaFrutosSecos, value);
        }

        private string _intoleranciaAlimentarOutras = string.Empty;
        public string IntoleranciaAlimentarOutras
        {
            get => _intoleranciaAlimentarOutras;
            set => SetProperty(ref _intoleranciaAlimentarOutras, value);
        }

        private string _alergiasMedicamentos = string.Empty;
        public string AlergiasMedicamentos
        {
            get => _alergiasMedicamentos;
            set => SetProperty(ref _alergiasMedicamentos, value);
        }

        private string _alergiasMedicamentosDetalhes = string.Empty;
        public string AlergiasMedicamentosDetalhes
        {
            get => _alergiasMedicamentosDetalhes;
            set => SetProperty(ref _alergiasMedicamentosDetalhes, value);
        }

        private string _alergiasAmbientais = string.Empty;
        public string AlergiasAmbientais
        {
            get => _alergiasAmbientais;
            set => SetProperty(ref _alergiasAmbientais, value);
        }

        private string _alergiasAmbientaisDetalhes = string.Empty;
        public string AlergiasAmbientaisDetalhes
        {
            get => _alergiasAmbientaisDetalhes;
            set => SetProperty(ref _alergiasAmbientaisDetalhes, value);
        }

        // Alergias ambientais específicas
        private bool _alergiaPolen;
        public bool AlergiaPolen
        {
            get => _alergiaPolen;
            set => SetProperty(ref _alergiaPolen, value);
        }

        private bool _alergiaAcaros;
        public bool AlergiaAcaros
        {
            get => _alergiaAcaros;
            set => SetProperty(ref _alergiaAcaros, value);
        }

        private bool _alergiaPelosAnimais;
        public bool AlergiaPelosAnimais
        {
            get => _alergiaPelosAnimais;
            set => SetProperty(ref _alergiaPelosAnimais, value);
        }

        private bool _alergiaPoeira;
        public bool AlergiaPoeira
        {
            get => _alergiaPoeira;
            set => SetProperty(ref _alergiaPoeira, value);
        }

        private bool _alergiaMofo;
        public bool AlergiaMofo
        {
            get => _alergiaMofo;
            set => SetProperty(ref _alergiaMofo, value);
        }

        private string _alergiasAmbientaisOutras = string.Empty;
        public string AlergiasAmbientaisOutras
        {
            get => _alergiasAmbientaisOutras;
            set => SetProperty(ref _alergiasAmbientaisOutras, value);
        }

        private string _alergiasPlantas = string.Empty;
        public string AlergiasPlantas
        {
            get => _alergiasPlantas;
            set => SetProperty(ref _alergiasPlantas, value);
        }

        private string _alergiasPlantasDetalhes = string.Empty;
        public string AlergiasPlantasDetalhes
        {
            get => _alergiasPlantasDetalhes;
            set => SetProperty(ref _alergiasPlantasDetalhes, value);
        }

        private string _outrasAlergias = string.Empty;
        public string OutrasAlergias
        {
            get => _outrasAlergias;
            set => SetProperty(ref _outrasAlergias, value);
        }

        private string _outrasAlergiasDetalhes = string.Empty;
        public string OutrasAlergiasDetalhes
        {
            get => _outrasAlergiasDetalhes;
            set => SetProperty(ref _outrasAlergiasDetalhes, value);
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

        // ========== SEÇÃO 5: OBJETIVOS ==========
        private string _objetivoConsulta = string.Empty;
        public string ObjetivoConsulta
        {
            get => _objetivoConsulta;
            set => SetProperty(ref _objetivoConsulta, value);
        }

        private string _expectativas = string.Empty;
        public string Expectativas
        {
            get => _expectativas;
            set => SetProperty(ref _expectativas, value);
        }

        private string _motivacoes = string.Empty;
        public string Motivacoes
        {
            get => _motivacoes;
            set => SetProperty(ref _motivacoes, value);
        }

        // ========== SEÇÃO 6: ESTADO GERAL ==========
        private string _qualidadeSono = string.Empty;
        public string QualidadeSono
        {
            get => _qualidadeSono;
            set => SetProperty(ref _qualidadeSono, value);
        }

        private string _nivelEnergia = string.Empty;
        public string NivelEnergia
        {
            get => _nivelEnergia;
            set => SetProperty(ref _nivelEnergia, value);
        }

        private string _estadoHumor = string.Empty;
        public string EstadoHumor
        {
            get => _estadoHumor;
            set => SetProperty(ref _estadoHumor, value);
        }

        private string _nivelStress = string.Empty;
        public string NivelStress
        {
            get => _nivelStress;
            set => SetProperty(ref _nivelStress, value);
        }

        private string _problemasSono = string.Empty;
        public string ProblemasSono
        {
            get => _problemasSono;
            set => SetProperty(ref _problemasSono, value);
        }

        // ========== SEÇÃO 7: SISTEMA CARDIOVASCULAR ==========
        private string _problemasCardiacos = string.Empty;
        public string ProblemasCardiacos
        {
            get => _problemasCardiacos;
            set => SetProperty(ref _problemasCardiacos, value);
        }

        private string _hipertensao = string.Empty;
        public string Hipertensao
        {
            get => _hipertensao;
            set => SetProperty(ref _hipertensao, value);
        }

        private string _problemasCirculatorios = string.Empty;
        public string ProblemasCirculatorios
        {
            get => _problemasCirculatorios;
            set => SetProperty(ref _problemasCirculatorios, value);
        }

        private string _colesterol = string.Empty;
        public string Colesterol
        {
            get => _colesterol;
            set => SetProperty(ref _colesterol, value);
        }

        // ========== SEÇÃO 8: SISTEMA RESPIRATÓRIO ==========
        private string _problemasRespiratorios = string.Empty;
        public string ProblemasRespiratorios
        {
            get => _problemasRespiratorios;
            set => SetProperty(ref _problemasRespiratorios, value);
        }

        private string _problemasRespiratoriosDetalhes = string.Empty;
        public string ProblemasRespiratoriosDetalhes
        {
            get => _problemasRespiratoriosDetalhes;
            set => SetProperty(ref _problemasRespiratoriosDetalhes, value);
        }

        private string _asma = string.Empty;
        public string Asma
        {
            get => _asma;
            set => SetProperty(ref _asma, value);
        }

        private string _bronquite = string.Empty;
        public string Bronquite
        {
            get => _bronquite;
            set => SetProperty(ref _bronquite, value);
        }

        private string _problemasPulmonares = string.Empty;
        public string ProblemasPulmonares
        {
            get => _problemasPulmonares;
            set => SetProperty(ref _problemasPulmonares, value);
        }

        // ========== SEÇÃO 9: SISTEMA DIGESTIVO ==========
        private string _problemasDigestivos = string.Empty;
        public string ProblemasDigestivos
        {
            get => _problemasDigestivos;
            set => SetProperty(ref _problemasDigestivos, value);
        }

        private string _problemasDigestivosDetalhes = string.Empty;
        public string ProblemasDigestivosDetalhes
        {
            get => _problemasDigestivosDetalhes;
            set => SetProperty(ref _problemasDigestivosDetalhes, value);
        }

        private string _refluxo = string.Empty;
        public string Refluxo
        {
            get => _refluxo;
            set => SetProperty(ref _refluxo, value);
        }

        private string _ulceras = string.Empty;
        public string Ulceras
        {
            get => _ulceras;
            set => SetProperty(ref _ulceras, value);
        }

        private string _intestino = string.Empty;
        public string Intestino
        {
            get => _intestino;
            set => SetProperty(ref _intestino, value);
        }

        private string _figado = string.Empty;
        public string Figado
        {
            get => _figado;
            set => SetProperty(ref _figado, value);
        }

        // ========== SEÇÃO 10: SISTEMA REPRODUTOR ==========
        private string _saudeReproductiva = string.Empty;
        public string SaudeReproductiva
        {
            get => _saudeReproductiva;
            set => SetProperty(ref _saudeReproductiva, value);
        }

        private string _cicloMenstrual = string.Empty;
        public string CicloMenstrual
        {
            get => _cicloMenstrual;
            set => SetProperty(ref _cicloMenstrual, value);
        }

        private string _menopausa = string.Empty;
        public string Menopausa
        {
            get => _menopausa;
            set => SetProperty(ref _menopausa, value);
        }

        private string _gravidez = string.Empty;
        public string Gravidez
        {
            get => _gravidez;
            set => SetProperty(ref _gravidez, value);
        }

        // ========== SEÇÃO 11: SISTEMA NERVOSO ==========
        private string _problemasNeurologicos = string.Empty;
        public string ProblemasNeurologicos
        {
            get => _problemasNeurologicos;
            set => SetProperty(ref _problemasNeurologicos, value);
        }

        private string _problemasNeurologicosDetalhes = string.Empty;
        public string ProblemasNeurologicosDetalhes
        {
            get => _problemasNeurologicosDetalhes;
            set => SetProperty(ref _problemasNeurologicosDetalhes, value);
        }

        private string _dorCabeca = string.Empty;
        public string DorCabeca
        {
            get => _dorCabeca;
            set => SetProperty(ref _dorCabeca, value);
        }

        private string _enxaquecas = string.Empty;
        public string Enxaquecas
        {
            get => _enxaquecas;
            set => SetProperty(ref _enxaquecas, value);
        }

        private string _problemasTonturas = string.Empty;
        public string ProblemasTonturas
        {
            get => _problemasTonturas;
            set => SetProperty(ref _problemasTonturas, value);
        }

        private string _problemasMemoria = string.Empty;
        public string ProblemasMemoria
        {
            get => _problemasMemoria;
            set => SetProperty(ref _problemasMemoria, value);
        }

        // ========== SEÇÃO 12: SISTEMA MUSCULOESQUELÉTICO ==========
        private string _doresArticulares = string.Empty;
        public string DoresArticulares
        {
            get => _doresArticulares;
            set => SetProperty(ref _doresArticulares, value);
        }

        private string _doresCostas = string.Empty;
        public string DoresCostas
        {
            get => _doresCostas;
            set => SetProperty(ref _doresCostas, value);
        }

        private string _artrite = string.Empty;
        public string Artrite
        {
            get => _artrite;
            set => SetProperty(ref _artrite, value);
        }

        private string _problemasOsseos = string.Empty;
        public string ProblemasOsseos
        {
            get => _problemasOsseos;
            set => SetProperty(ref _problemasOsseos, value);
        }

        private string _problemasMusculos = string.Empty;
        public string ProblemasMusculos
        {
            get => _problemasMusculos;
            set => SetProperty(ref _problemasMusculos, value);
        }

        // ========== SEÇÃO 13: SISTEMA URINÁRIO ==========
        private string _infeccoesUrinaras = string.Empty;
        public string InfeccoesUrinaras
        {
            get => _infeccoesUrinaras;
            set => SetProperty(ref _infeccoesUrinaras, value);
        }

        private string _problemasRins = string.Empty;
        public string ProblemasRins
        {
            get => _problemasRins;
            set => SetProperty(ref _problemasRins, value);
        }

        private string _incontinencia = string.Empty;
        public string Incontinencia
        {
            get => _incontinencia;
            set => SetProperty(ref _incontinencia, value);
        }

        // ========== SEÇÃO 14: PELE E ANEXOS ==========
        private string _alergiasPele = string.Empty;
        public string AlergiasPele
        {
            get => _alergiasPele;
            set => SetProperty(ref _alergiasPele, value);
        }

        private string _eczema = string.Empty;
        public string Eczema
        {
            get => _eczema;
            set => SetProperty(ref _eczema, value);
        }

        private string _psoriase = string.Empty;
        public string Psoriase
        {
            get => _psoriase;
            set => SetProperty(ref _psoriase, value);
        }

        // ========== SEÇÃO 15: ÓRGÃOS DOS SENTIDOS ==========
        private string _problemasVisao = string.Empty;
        public string ProblemasVisao
        {
            get => _problemasVisao;
            set => SetProperty(ref _problemasVisao, value);
        }

        private string _problemasAudicao = string.Empty;
        public string ProblemasAudicao
        {
            get => _problemasAudicao;
            set => SetProperty(ref _problemasAudicao, value);
        }

        private string _problemasOncologicos = string.Empty;
        public string ProblemasOncologicos
        {
            get => _problemasOncologicos;
            set => SetProperty(ref _problemasOncologicos, value);
        }

        private string _problemasReumaticos = string.Empty;
        public string ProblemasReumaticos
        {
            get => _problemasReumaticos;
            set => SetProperty(ref _problemasReumaticos, value);
        }

        private string _problemasOlfato = string.Empty;
        public string ProblemasOlfato
        {
            get => _problemasOlfato;
            set => SetProperty(ref _problemasOlfato, value);
        }

        private string _problemasPaladar = string.Empty;
        public string ProblemasPaladar
        {
            get => _problemasPaladar;
            set => SetProperty(ref _problemasPaladar, value);
        }

        // ========== SEÇÃO 16: OUTROS PROBLEMAS ==========
        private string _outrosProblemas = string.Empty;
        public string OutrosProblemas
        {
            get => _outrosProblemas;
            set => SetProperty(ref _outrosProblemas, value);
        }

        private string _observacoesGerais = string.Empty;
        public string ObservacoesGerais
        {
            get => _observacoesGerais;
            set => SetProperty(ref _observacoesGerais, value);
        }

        private string _historicoFamiliar = string.Empty;
        public string HistoricoFamiliar
        {
            get => _historicoFamiliar;
            set => SetProperty(ref _historicoFamiliar, value);
        }

        // ========== PROPRIEDADES ADICIONAIS PARA NOVOS SISTEMAS ==========
        
        // Sistema Musculoesquelético
        private string _problemasMusculoesqueleticos = string.Empty;
        public string ProblemasMusculoesqueleticos
        {
            get => _problemasMusculoesqueleticos;
            set => SetProperty(ref _problemasMusculoesqueleticos, value);
        }

        private string _problemasMusculoesqueleticosDetalhes = string.Empty;
        public string ProblemasMusculoesqueleticosDetalhes
        {
            get => _problemasMusculoesqueleticosDetalhes;
            set => SetProperty(ref _problemasMusculoesqueleticosDetalhes, value);
        }

        // Sistema Urinário
        private string _problemasUrinarios = string.Empty;
        public string ProblemasUrinarios
        {
            get => _problemasUrinarios;
            set => SetProperty(ref _problemasUrinarios, value);
        }

        private string _problemasUrinariosDetalhes = string.Empty;
        public string ProblemasUrinariosDetalhes
        {
            get => _problemasUrinariosDetalhes;
            set => SetProperty(ref _problemasUrinariosDetalhes, value);
        }

        // Sistema Imunitário
        private string _problemasImunitarios = string.Empty;
        public string ProblemasImunitarios
        {
            get => _problemasImunitarios;
            set => SetProperty(ref _problemasImunitarios, value);
        }

        private string _problemasImunitariosDetalhes = string.Empty;
        public string ProblemasImunitariosDetalhes
        {
            get => _problemasImunitariosDetalhes;
            set => SetProperty(ref _problemasImunitariosDetalhes, value);
        }

        // Sistema Endócrino
        private string _problemasEndocrinos = string.Empty;
        public string ProblemasEndocrinos
        {
            get => _problemasEndocrinos;
            set => SetProperty(ref _problemasEndocrinos, value);
        }

        private string _problemasEndocrinosDetalhes = string.Empty;
        public string ProblemasEndocrinosDetalhes
        {
            get => _problemasEndocrinosDetalhes;
            set => SetProperty(ref _problemasEndocrinosDetalhes, value);
        }

        // Pele e Anexos
        private string _problemasPele = string.Empty;
        public string ProblemasPele
        {
            get => _problemasPele;
            set => SetProperty(ref _problemasPele, value);
        }

        private string _problemasPeleDetalhes = string.Empty;
        public string ProblemasPeleDetalhes
        {
            get => _problemasPeleDetalhes;
            set => SetProperty(ref _problemasPeleDetalhes, value);
        }

        // Órgãos dos Sentidos - Detalhes
        private string _problemasVisaoDetalhes = string.Empty;
        public string ProblemasVisaoDetalhes
        {
            get => _problemasVisaoDetalhes;
            set => SetProperty(ref _problemasVisaoDetalhes, value);
        }

        private string _problemasAudicaoDetalhes = string.Empty;
        public string ProblemasAudicaoDetalhes
        {
            get => _problemasAudicaoDetalhes;
            set => SetProperty(ref _problemasAudicaoDetalhes, value);
        }

        private string _problemasOncologicosDetalhes = string.Empty;
        public string ProblemasOncologicosDetalhes
        {
            get => _problemasOncologicosDetalhes;
            set => SetProperty(ref _problemasOncologicosDetalhes, value);
        }

        private string _problemasReumaticosDetalhes = string.Empty;
        public string ProblemasReumaticosDetalhes
        {
            get => _problemasReumaticosDetalhes;
            set => SetProperty(ref _problemasReumaticosDetalhes, value);
        }

        #region Consentimentos

        // Naturopatia
        private bool _naturopatiaCompreendeNatureza = false;
        public bool NaturopatiaCompreendeNatureza
        {
            get => _naturopatiaCompreendeNatureza;
            set => SetProperty(ref _naturopatiaCompreendeNatureza, value);
        }

        private bool _naturopatiaObjetivosExplicados = false;
        public bool NaturopatiaObjetivosExplicados
        {
            get => _naturopatiaObjetivosExplicados;
            set => SetProperty(ref _naturopatiaObjetivosExplicados, value);
        }

        private bool _naturopatiaRiscosDiscutidos = false;
        public bool NaturopatiaRiscosDiscutidos
        {
            get => _naturopatiaRiscosDiscutidos;
            set => SetProperty(ref _naturopatiaRiscosDiscutidos, value);
        }

        private bool _naturopatiaInformacaoPatologias = false;
        public bool NaturopatiaInformacaoPatologias
        {
            get => _naturopatiaInformacaoPatologias;
            set => SetProperty(ref _naturopatiaInformacaoPatologias, value);
        }

        private bool _naturopatiaAlternativasDiscutidas = false;
        public bool NaturopatiaAlternativasDiscutidas
        {
            get => _naturopatiaAlternativasDiscutidas;
            set => SetProperty(ref _naturopatiaAlternativasDiscutidas, value);
        }

        private bool _naturopatiaAutorizacaoCorresponsabilidade = false;
        public bool NaturopatiaAutorizacaoCorresponsabilidade
        {
            get => _naturopatiaAutorizacaoCorresponsabilidade;
            set => SetProperty(ref _naturopatiaAutorizacaoCorresponsabilidade, value);
        }

        private bool _naturopatiaRevogavel = false;
        public bool NaturopatiaRevogavel
        {
            get => _naturopatiaRevogavel;
            set => SetProperty(ref _naturopatiaRevogavel, value);
        }

        private string? _naturopatiaAssinatura = null;
        public string? NaturopatiaAssinatura
        {
            get => _naturopatiaAssinatura;
            set => SetProperty(ref _naturopatiaAssinatura, value);
        }

        // Osteopatia
        private bool _osteopatiaTecnicasExplicadas = false;
        public bool OsteopatiaTecnicasExplicadas
        {
            get => _osteopatiaTecnicasExplicadas;
            set => SetProperty(ref _osteopatiaTecnicasExplicadas, value);
        }

        private bool _osteopatiaContraindicacoesDiscutidas = false;
        public bool OsteopatiaContraindicacoesDiscutidas
        {
            get => _osteopatiaContraindicacoesDiscutidas;
            set => SetProperty(ref _osteopatiaContraindicacoesDiscutidas, value);
        }

        private bool _osteopatiaRiscosExplicados = false;
        public bool OsteopatiaRiscosExplicados
        {
            get => _osteopatiaRiscosExplicados;
            set => SetProperty(ref _osteopatiaRiscosExplicados, value);
        }

        private bool _osteopatiaAutorizoContactoFisico = false;
        public bool OsteopatiaAutorizoContactoFisico
        {
            get => _osteopatiaAutorizoContactoFisico;
            set => SetProperty(ref _osteopatiaAutorizoContactoFisico, value);
        }

        private bool _osteopatiaPossoInterromper = false;
        public bool OsteopatiaPossoInterromper
        {
            get => _osteopatiaPossoInterromper;
            set => SetProperty(ref _osteopatiaPossoInterromper, value);
        }

        private bool _osteopatiaRevogavel = false;
        public bool OsteopatiaRevogavel
        {
            get => _osteopatiaRevogavel;
            set => SetProperty(ref _osteopatiaRevogavel, value);
        }

        private string? _osteopatiaAssinatura = null;
        public string? OsteopatiaAssinatura
        {
            get => _osteopatiaAssinatura;
            set => SetProperty(ref _osteopatiaAssinatura, value);
        }

        // Iridologia
        private bool _iridologiaNaturezaNaoInvasiva = false;
        public bool IridologiaNaturezaNaoInvasiva
        {
            get => _iridologiaNaturezaNaoInvasiva;
            set => SetProperty(ref _iridologiaNaturezaNaoInvasiva, value);
        }

        private bool _iridologiaAutorizoCapturaImagens = false;
        public bool IridologiaAutorizoCapturaImagens
        {
            get => _iridologiaAutorizoCapturaImagens;
            set => SetProperty(ref _iridologiaAutorizoCapturaImagens, value);
        }

        private bool _iridologiaCompreensoLimitacoes = false;
        public bool IridologiaCompreensoLimitacoes
        {
            get => _iridologiaCompreensoLimitacoes;
            set => SetProperty(ref _iridologiaCompreensoLimitacoes, value);
        }

        private bool _iridologiaRevogavel = false;
        public bool IridologiaRevogavel
        {
            get => _iridologiaRevogavel;
            set => SetProperty(ref _iridologiaRevogavel, value);
        }

        private string? _iridologiaAssinatura = null;
        public string? IridologiaAssinatura
        {
            get => _iridologiaAssinatura;
            set => SetProperty(ref _iridologiaAssinatura, value);
        }

        // Medicina Quântica
        private bool _medicinaQuanticaAbordagemComplementar = false;
        public bool MedicinaQuanticaAbordagemComplementar
        {
            get => _medicinaQuanticaAbordagemComplementar;
            set => SetProperty(ref _medicinaQuanticaAbordagemComplementar, value);
        }

        private bool _medicinaQuanticaNaturezaProcedimentos = false;
        public bool MedicinaQuanticaNaturezaProcedimentos
        {
            get => _medicinaQuanticaNaturezaProcedimentos;
            set => SetProperty(ref _medicinaQuanticaNaturezaProcedimentos, value);
        }

        private bool _medicinaQuanticaPossiveisReacoes = false;
        public bool MedicinaQuanticaPossiveisReacoes
        {
            get => _medicinaQuanticaPossiveisReacoes;
            set => SetProperty(ref _medicinaQuanticaPossiveisReacoes, value);
        }

        private bool _medicinaQuanticaContraindicacoes = false;
        public bool MedicinaQuanticaContraindicacoes
        {
            get => _medicinaQuanticaContraindicacoes;
            set => SetProperty(ref _medicinaQuanticaContraindicacoes, value);
        }

        private bool _medicinaQuanticaLiberdadeInterromper = false;
        public bool MedicinaQuanticaLiberdadeInterromper
        {
            get => _medicinaQuanticaLiberdadeInterromper;
            set => SetProperty(ref _medicinaQuanticaLiberdadeInterromper, value);
        }

        private bool _medicinaQuanticaRevogavel = false;
        public bool MedicinaQuanticaRevogavel
        {
            get => _medicinaQuanticaRevogavel;
            set => SetProperty(ref _medicinaQuanticaRevogavel, value);
        }

        private string? _medicinaQuanticaAssinatura = null;
        public string? MedicinaQuanticaAssinatura
        {
            get => _medicinaQuanticaAssinatura;
            set => SetProperty(ref _medicinaQuanticaAssinatura, value);
        }

        // RGPD
        private bool _rgpdInformacaoResponsavel = false;
        public bool RgpdInformacaoResponsavel
        {
            get => _rgpdInformacaoResponsavel;
            set => SetProperty(ref _rgpdInformacaoResponsavel, value);
        }

        private bool _rgpdDireitosAcesso = false;
        public bool RgpdDireitosAcesso
        {
            get => _rgpdDireitosAcesso;
            set => SetProperty(ref _rgpdDireitosAcesso, value);
        }

        private bool _rgpdConsentimentoExplicito = false;
        public bool RgpdConsentimentoExplicito
        {
            get => _rgpdConsentimentoExplicito;
            set => SetProperty(ref _rgpdConsentimentoExplicito, value);
        }

        private bool _rgpdOpcaoNaoMarketing = false;
        public bool RgpdOpcaoNaoMarketing
        {
            get => _rgpdOpcaoNaoMarketing;
            set => SetProperty(ref _rgpdOpcaoNaoMarketing, value);
        }

        private bool _rgpdRevogavel = false;
        public bool RgpdRevogavel
        {
            get => _rgpdRevogavel;
            set => SetProperty(ref _rgpdRevogavel, value);
        }

        private string? _rgpdAssinatura = null;
        public string? RgpdAssinatura
        {
            get => _rgpdAssinatura;
            set => SetProperty(ref _rgpdAssinatura, value);
        }

        // Assinatura geral do questionário
        private string? _assinaturaQuestionarioCompleto = null;
        public string? AssinaturaQuestionarioCompleto
        {
            get => _assinaturaQuestionarioCompleto;
            set => SetProperty(ref _assinaturaQuestionarioCompleto, value);
        }

        private bool _questionarioAssinado = false;
        public bool QuestionarioAssinado
        {
            get => _questionarioAssinado;
            set => SetProperty(ref _questionarioAssinado, value);
        }

        #endregion

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

        // Comandos para assinatura de consentimentos
        public ICommand AssinarNaturopatiaCommand { get; private set; } = null!;
        public ICommand AssinarOsteopatiaCommand { get; private set; } = null!;
        public ICommand AssinarIridologiaCommand { get; private set; } = null!;
        public ICommand AssinarMedicinaQuanticaCommand { get; private set; } = null!;
        public ICommand AssinarRgpdCommand { get; private set; } = null!;
        public ICommand AssinarQuestionarioCompletoCommand { get; private set; } = null!;

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
            
            // Comandos de consentimentos
            AssinarNaturopatiaCommand = new RelayCommand(async () => await AssinarConsentimento("Naturopatia"));
            AssinarOsteopatiaCommand = new RelayCommand(async () => await AssinarConsentimento("Osteopatia"));
            AssinarIridologiaCommand = new RelayCommand(async () => await AssinarConsentimento("Iridologia"));
            AssinarMedicinaQuanticaCommand = new RelayCommand(async () => await AssinarConsentimento("MedicinaQuantica"));
            AssinarRgpdCommand = new RelayCommand(async () => await AssinarConsentimento("RGPD"));
            AssinarQuestionarioCompletoCommand = new RelayCommand(async () => await AssinarQuestionarioCompleto());
            
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
                
                if (PacienteId <= 0)
                {
                    MessageBox.Show("Por favor, selecione um paciente primeiro.", 
                                   "Paciente não selecionado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                App.DebugLog($"=== CARREGANDO QUESTIONÁRIO PARA PACIENTE {PacienteId} ===");
                
                using var context = _serviceProvider.GetRequiredService<BioDeskDbContext>();
                
                // Buscar questionário mais recente do paciente
                var questionario = await context.QuestionariosSaude
                    .Where(q => q.PacienteId == PacienteId)
                    .OrderByDescending(q => q.DataPreenchimento)
                    .FirstOrDefaultAsync();

                if (questionario == null)
                {
                    MessageBox.Show("Nenhum questionário encontrado para este paciente.", 
                                   "Questionário não encontrado", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                App.DebugLog($"Questionário encontrado! ID: {questionario.Id}, Data: {questionario.DataPreenchimento}");
                
                // Carregar dados na interface
                CarregarDadosNaInterface(questionario);
                
                MessageBox.Show($"Questionário carregado com sucesso!\nData: {questionario.DataPreenchimento:dd/MM/yyyy HH:mm}", 
                               "Questionário carregado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                App.DebugLog($"ERRO ao carregar questionário: {ex.Message}");
                MessageBox.Show($"Erro ao carregar questionário: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void CarregarDadosNaInterface(QuestionarioSaude questionario)
        {
            App.DebugLog("=== CARREGANDO DADOS NA INTERFACE ===");
            
            // Informações médicas gerais
            DoencaCronica = questionario.CondicoesCronicas ?? string.Empty;
            SintomasAtuais = questionario.SintomasAtuais ?? string.Empty;
            MedicacaoAtual = questionario.MedicacaoAtual ?? string.Empty;
            
            // Doenças crónicas específicas (novos campos boolean)
            DoencaHipertensao = questionario.DoencaHipertensao;
            DoencaCardiopatia = questionario.DoencaCardiopatia;
            DoencaDiabetesTipo1 = questionario.DoencaDiabetesTipo1;
            DoencaDiabetesTipo2 = questionario.DoencaDiabetesTipo2;
            DoencaTiroideia = questionario.DoencaTiroideia;
            DoencaDislipidemia = questionario.DoencaDislipidemia;
            DoencaRenal = questionario.DoencaRenal;
            DoencaHepatica = questionario.DoencaHepatica;
            DoencaAutoimune = questionario.DoencaAutoimune;
            DoencaOsteoporose = questionario.DoencaOsteoporose;
            DoencaAsmaDPOC = questionario.DoencaAsmaDPOC;
            DoencaOncologia = questionario.DoencaOncologia;
            
            // Alergias - campos string (compatibilidade)
            AlergiasAlimentares = questionario.AlergiasAlimentos ?? string.Empty;
            AlergiasMedicamentos = questionario.AlergiasMedicamentos ?? string.Empty;
            AlergiasAmbientais = questionario.AlergiasAmbientais ?? string.Empty;
            AlergiasPlantas = questionario.AlergiasPlantas ?? string.Empty;
            OutrasAlergias = questionario.AlergiasSupplementos ?? string.Empty;
            
            // Intolerâncias alimentares específicas (novos campos boolean)
            IntoleranciaGluten = questionario.IntoleranciaGluten;
            IntoleranciaLactose = questionario.IntoleranciaLactose;
            IntoleranciaProteinaLeite = questionario.IntoleranciaProteínaLeite;
            IntoleranciaOvos = questionario.IntoleranciaOvos;
            IntoleranciaMarisco = questionario.IntoleranciaMarisco;
            IntoleranciaFrutosSecos = questionario.IntoleranciaFrutosSecos;
            IntoleranciaAlimentarOutras = questionario.IntoleranciaAlimentarOutras ?? string.Empty;
            
            // Alergias ambientais específicas (novos campos boolean)
            AlergiaPolen = questionario.AlergiaPolen;
            AlergiaAcaros = questionario.AlergiaAcaros;
            AlergiaPelosAnimais = questionario.AlergiaPelosAnimais;
            AlergiaPoeira = questionario.AlergiaPoeira;
            AlergiaMofo = questionario.AlergiaMofo;
            AlergiasAmbientaisOutras = questionario.AlergiasAmbientaisOutras ?? string.Empty;
            
            App.DebugLog("Dados carregados na interface com sucesso!");
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
                
                // Doenças crónicas específicas (checkboxes)
                questionario.DoencaHipertensao = DoencaHipertensao;
                questionario.DoencaCardiopatia = DoencaCardiopatia;
                questionario.DoencaDiabetesTipo1 = DoencaDiabetesTipo1;
                questionario.DoencaDiabetesTipo2 = DoencaDiabetesTipo2;
                questionario.DoencaTiroideia = DoencaTiroideia;
                questionario.DoencaDislipidemia = DoencaDislipidemia;
                questionario.DoencaRenal = DoencaRenal;
                questionario.DoencaHepatica = DoencaHepatica;
                questionario.DoencaAutoimune = DoencaAutoimune;
                questionario.DoencaOsteoporose = DoencaOsteoporose;
                questionario.DoencaAsmaDPOC = DoencaAsmaDPOC;
                questionario.DoencaOncologia = DoencaOncologia;
                
                questionario.AlergiasAlimentos = AlergiasAlimentares;
                questionario.AlergiasMedicamentos = AlergiasMedicamentos;
                questionario.AlergiasAmbientais = AlergiasAmbientais;
                
                // Intolerâncias alimentares específicas (checkboxes)
                questionario.IntoleranciaGluten = IntoleranciaGluten;
                questionario.IntoleranciaLactose = IntoleranciaLactose;
                questionario.IntoleranciaProteínaLeite = IntoleranciaProteinaLeite;
                questionario.IntoleranciaOvos = IntoleranciaOvos;
                questionario.IntoleranciaMarisco = IntoleranciaMarisco;
                questionario.IntoleranciaFrutosSecos = IntoleranciaFrutosSecos;
                questionario.IntoleranciaAlimentarOutras = IntoleranciaAlimentarOutras;
                
                // Alergias ambientais específicas
                questionario.AlergiaPolen = AlergiaPolen;
                questionario.AlergiaAcaros = AlergiaAcaros;
                questionario.AlergiaPelosAnimais = AlergiaPelosAnimais;
                questionario.AlergiaPoeira = AlergiaPoeira;
                questionario.AlergiaMofo = AlergiaMofo;
                questionario.AlergiasAmbientaisOutras = AlergiasAmbientaisOutras;
                
                questionario.AlergiasPlantas = AlergiasPlantas;
                questionario.AlergiasSupplementos = OutrasAlergias;
                questionario.HistoricoCirurgias = HistoricoCirurgico;
                questionario.HistoricoFraturas = Fraturas;
                questionario.DetalhesEstiloVida = $"Atividade Física: {AtividadeFisica}; Tabagismo: {Tabagismo}; Álcool: {ConsumoAlcool}; Hábitos Alimentares: {HabitosAlimentares}";
                
                // ========== SEÇÃO 5: OBJETIVOS ==========
                questionario.QueixaPrincipal = ObjetivoConsulta;
                questionario.ObjetivoOutro = $"Expectativas: {Expectativas}; Motivações: {Motivacoes}";
                
                // ========== SEÇÃO 6: ESTADO GERAL ==========
                questionario.SaudeGlobal = EstadoHumor;
                questionario.HorasSono = QualidadeSono;
                questionario.ExercicioFisico = NivelEnergia;
                questionario.DetalhesEstiloVida += $"; Sono: {QualidadeSono}; Energia: {NivelEnergia}; Humor: {EstadoHumor}; Stress: {NivelStress}; Problemas Sono: {ProblemasSono}";
                
                // ========== SEÇÃO 7: SISTEMA CARDIOVASCULAR ==========
                questionario.DoencaCardiopatia = !string.IsNullOrWhiteSpace(ProblemasCardiacos);
                questionario.DiagnosticosMedicosAtuais = $"Cardíacos: {ProblemasCardiacos}; Hipertensão: {Hipertensao}; Circulatórios: {ProblemasCirculatorios}; Colesterol: {Colesterol}";
                questionario.DoencaHipertensao = Hipertensao?.Contains("Sim") == true;
                questionario.DoencaDislipidemia = Colesterol?.Contains("Elevado") == true;
                
                // ========== SEÇÃO 8: SISTEMA RESPIRATÓRIO ==========
                questionario.DoencaAsmaDPOC = !string.IsNullOrWhiteSpace(ProblemasRespiratorios) || Asma?.Contains("Sim") == true;
                var respiratorioDetalhes = ProblemasRespiratorios;
                if (!string.IsNullOrWhiteSpace(ProblemasRespiratoriosDetalhes)) {
                    respiratorioDetalhes += $" - Detalhes: {ProblemasRespiratoriosDetalhes}";
                }
                questionario.DiagnosticosMedicosAtuais += $"; Respiratórios: {respiratorioDetalhes}; Asma: {Asma}; Bronquite: {Bronquite}; Pulmonares: {ProblemasPulmonares}";
                
                // ========== SEÇÃO 9: SISTEMA DIGESTIVO ==========
                var digestivoDetalhes = ProblemasDigestivos;
                if (!string.IsNullOrWhiteSpace(ProblemasDigestivosDetalhes)) {
                    digestivoDetalhes += $" - Detalhes: {ProblemasDigestivosDetalhes}";
                }
                questionario.DiagnosticosMedicosAtuais += $"; Digestivos: {digestivoDetalhes}; Refluxo: {Refluxo}; Úlceras: {Ulceras}; Intestino: {Intestino}; Fígado: {Figado}";
                
                // ========== SEÇÃO 10: SISTEMA REPRODUTOR ==========
                questionario.DiagnosticosMedicosAtuais += $"; Reprodutivo: {SaudeReproductiva}; Ciclo: {CicloMenstrual}; Menopausa: {Menopausa}; Gravidez: {Gravidez}";
                if (!string.IsNullOrWhiteSpace(Gravidez)) {
                    questionario.GravidezAleitamento = Gravidez.Contains("grávida") ? "Grávida" : "Não";
                }
                
                // ========== SEÇÃO 11: SISTEMA NERVOSO ==========
                var neurologicoDetalhes = ProblemasNeurologicos;
                if (!string.IsNullOrWhiteSpace(ProblemasNeurologicosDetalhes)) {
                    neurologicoDetalhes += $" - Detalhes: {ProblemasNeurologicosDetalhes}";
                }
                questionario.DiagnosticosMedicosAtuais += $"; Neurológicos: {neurologicoDetalhes}; Dor Cabeça: {DorCabeca}; Enxaquecas: {Enxaquecas}; Tonturas: {ProblemasTonturas}; Memória: {ProblemasMemoria}";
                
                // ========== DOENÇA CRÓNICA COM DETALHES ==========
                var doencaCronicaCompleta = DoencaCronica;
                if (!string.IsNullOrWhiteSpace(DoencaCronicaDetalhes)) {
                    doencaCronicaCompleta += $" - Detalhes: {DoencaCronicaDetalhes}";
                }
                questionario.CondicoesCronicas = doencaCronicaCompleta;
                
                // ========== SEÇÃO 12: SISTEMA MUSCULOESQUELÉTICO ==========
                questionario.DiagnosticosMedicosAtuais += $"; Articulares: {DoresArticulares}; Costas: {DoresCostas}; Artrite: {Artrite}; Ósseos: {ProblemasOsseos}; Musculares: {ProblemasMusculos}";
                if (Artrite?.Contains("Artrite") == true) {
                    questionario.DoencaAutoimune = true;
                }
                
                // ========== SEÇÃO 13: SISTEMA URINÁRIO ==========
                questionario.DoencaRenal = !string.IsNullOrWhiteSpace(ProblemasUrinarios) || !string.IsNullOrWhiteSpace(ProblemasRins);
                questionario.DiagnosticosMedicosAtuais += $"; Urinários: {ProblemasUrinarios}; Infecções: {InfeccoesUrinaras}; Rins: {ProblemasRins}; Incontinência: {Incontinencia}";
                
                // ========== SEÇÃO 14: PELE E ANEXOS ==========
                questionario.DiagnosticosMedicosAtuais += $"; Pele: {ProblemasPele}; Alergias Pele: {AlergiasPele}; Eczema: {Eczema}; Psoríase: {Psoriase}";
                
                // ========== SEÇÃO 15: ÓRGÃOS DOS SENTIDOS ==========
                questionario.DiagnosticosMedicosAtuais += $"; Visão: {ProblemasVisao}; Audição: {ProblemasAudicao}; Olfato: {ProblemasOlfato}; Paladar: {ProblemasPaladar}";
                
                // ========== SEÇÃO 16: OUTROS PROBLEMAS ==========
                questionario.DiagnosticosMedicosAtuais += $"; Outros: {OutrosProblemas}; Histórico Familiar: {HistoricoFamiliar}; Observações: {ObservacoesGerais}";
                
                questionario.DataUltimaAtualizacao = DateTime.Now;

                // Definir campos boolean baseado se há texto
                questionario.TomaMedicacao = !string.IsNullOrWhiteSpace(MedicacaoAtual);
                questionario.JaFezCirurgias = !string.IsNullOrWhiteSpace(HistoricoCirurgico);
                questionario.JaTeveFraturas = !string.IsNullOrWhiteSpace(Fraturas);
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

                // Salvar consentimentos
                App.DebugLog("Salvando consentimentos...");
                await SalvarConsentimentos();
                App.DebugLog("Consentimentos salvos com sucesso!");

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

        #region Métodos de Consentimentos

        private Task AssinarConsentimento(string tipoConsentimento)
        {
            return Task.Run(() =>
            {
                try
                {
                    // Simular abertura do SignatureCanvas
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var signatureWindow = new Window
                        {
                            Title = $"Assinar {tipoConsentimento}",
                            Width = 600,
                            Height = 400,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };

                        var signatureCanvas = new Controls.SignatureCanvas();
                        signatureWindow.Content = signatureCanvas;

                        signatureCanvas.SignatureCompleted += (sender, args) =>
                        {
                            // Converter assinatura para Base64
                            string base64Signature = Convert.ToBase64String(args.SignatureData);
                            
                            // Atualizar a propriedade correspondente
                            switch (tipoConsentimento)
                            {
                                case "Naturopatia":
                                    NaturopatiaAssinatura = base64Signature;
                                    break;
                                case "Osteopatia":
                                    OsteopatiaAssinatura = base64Signature;
                                    break;
                                case "Iridologia":
                                    IridologiaAssinatura = base64Signature;
                                    break;
                                case "MedicinaQuantica":
                                    MedicinaQuanticaAssinatura = base64Signature;
                                    break;
                                case "RGPD":
                                    RgpdAssinatura = base64Signature;
                                    break;
                            }

                            signatureWindow.DialogResult = true;
                            signatureWindow.Close();
                        };

                        signatureCanvas.SignatureCancelled += (sender, args) =>
                        {
                            signatureWindow.DialogResult = false;
                            signatureWindow.Close();
                        };

                        var result = signatureWindow.ShowDialog();
                        
                        if (result == true)
                        {
                            MessageBox.Show($"Consentimento {tipoConsentimento} assinado com sucesso!", 
                                           "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    });
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show($"Erro ao assinar consentimento {tipoConsentimento}: {ex.Message}", 
                                       "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            });
        }

        private Task AssinarQuestionarioCompleto()
        {
            return Task.Run(() =>
            {
                try
                {
                    // Verificar se consentimentos obrigatórios foram preenchidos
                    if (!ValidarConsentimentosObrigatorios())
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show("Complete todos os consentimentos obrigatórios antes de assinar o questionário completo.", 
                                           "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                        });
                        return;
                    }

                    // Simular abertura do SignatureCanvas
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var signatureWindow = new Window
                        {
                            Title = "Assinar Questionário Completo",
                            Width = 600,
                            Height = 400,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };

                        var signatureCanvas = new Controls.SignatureCanvas();
                        signatureWindow.Content = signatureCanvas;

                        signatureCanvas.SignatureCompleted += (sender, args) =>
                        {
                            AssinaturaQuestionarioCompleto = Convert.ToBase64String(args.SignatureData);
                            QuestionarioAssinado = true;
                            signatureWindow.DialogResult = true;
                            signatureWindow.Close();
                        };

                        signatureCanvas.SignatureCancelled += (sender, args) =>
                        {
                            signatureWindow.DialogResult = false;
                            signatureWindow.Close();
                        };

                        var result = signatureWindow.ShowDialog();
                        
                        if (result == true)
                        {
                            MessageBox.Show("Questionário assinado com sucesso!", 
                                           "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    });
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show($"Erro ao assinar questionário: {ex.Message}", 
                                       "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            });
        }

        private bool ValidarConsentimentosObrigatorios()
        {
            // Implementar validação de consentimentos obrigatórios
            // Por exemplo, RGPD é sempre obrigatório
            return RgpdConsentimentoExplicito && !string.IsNullOrEmpty(RgpdAssinatura);
        }

        private async Task SalvarConsentimentos()
        {
            try
            {
                // Usar o contexto que já pode estar injetado ou criar um novo com options
                var optionsBuilder = new DbContextOptionsBuilder<BioDeskDbContext>();
                optionsBuilder.UseSqlite("Data Source=biodesk.db");
                
                using var context = new BioDeskDbContext(optionsBuilder.Options);
                
                // Verificar se já existe um consentimento para este paciente
                var consentimentoExistente = await context.ConsentimentosInformados
                    .FirstOrDefaultAsync(c => c.PacienteId == PacienteId);

                if (consentimentoExistente == null)
                {
                    consentimentoExistente = new ConsentimentoInformado
                    {
                        PacienteId = PacienteId,
                        DataConsentimento = DateTime.Now,
                        ConteudoConsentimento = "Questionário completo com consentimentos"
                    };
                    context.ConsentimentosInformados.Add(consentimentoExistente);
                }

                // Atualizar campos de consentimentos
                consentimentoExistente.NaturopatiaCompreendeNatureza = NaturopatiaCompreendeNatureza;
                consentimentoExistente.NaturopatiaObjetivosExplicados = NaturopatiaObjetivosExplicados;
                consentimentoExistente.NaturopatiaRiscosDiscutidos = NaturopatiaRiscosDiscutidos;
                consentimentoExistente.NaturopatiaInformacaoPatologias = NaturopatiaInformacaoPatologias;
                consentimentoExistente.NaturopatiaAlternativasDiscutidas = NaturopatiaAlternativasDiscutidas;
                consentimentoExistente.NaturopatiaAutorizacaoCorresponsabilidade = NaturopatiaAutorizacaoCorresponsabilidade;
                consentimentoExistente.NaturopatiaRevogavel = NaturopatiaRevogavel;
                consentimentoExistente.NaturopatiaAssinatura = NaturopatiaAssinatura;
                consentimentoExistente.NaturopatiaDataAssinatura = !string.IsNullOrEmpty(NaturopatiaAssinatura) ? DateTime.Now : null;

                consentimentoExistente.OsteopatiaTecnicasExplicadas = OsteopatiaTecnicasExplicadas;
                consentimentoExistente.OsteopatiaContraindicacoesDiscutidas = OsteopatiaContraindicacoesDiscutidas;
                consentimentoExistente.OsteopatiaRiscosExplicados = OsteopatiaRiscosExplicados;
                consentimentoExistente.OsteopatiaAutorizoContactoFisico = OsteopatiaAutorizoContactoFisico;
                consentimentoExistente.OsteopatiaPossoInterromper = OsteopatiaPossoInterromper;
                consentimentoExistente.OsteopatiaRevogavel = OsteopatiaRevogavel;
                consentimentoExistente.OsteopatiaAssinatura = OsteopatiaAssinatura;
                consentimentoExistente.OsteopatiaDataAssinatura = !string.IsNullOrEmpty(OsteopatiaAssinatura) ? DateTime.Now : null;

                consentimentoExistente.IridologiaNaturezaNaoInvasiva = IridologiaNaturezaNaoInvasiva;
                consentimentoExistente.IridologiaAutorizoCapturaImagens = IridologiaAutorizoCapturaImagens;
                consentimentoExistente.IridologiaCompreensoLimitacoes = IridologiaCompreensoLimitacoes;
                consentimentoExistente.IridologiaRevogavel = IridologiaRevogavel;
                consentimentoExistente.IridologiaAssinatura = IridologiaAssinatura;
                consentimentoExistente.IridologiaDataAssinatura = !string.IsNullOrEmpty(IridologiaAssinatura) ? DateTime.Now : null;

                consentimentoExistente.MedicinaQuanticaAbordagemComplementar = MedicinaQuanticaAbordagemComplementar;
                consentimentoExistente.MedicinaQuanticaNaturezaProcedimentos = MedicinaQuanticaNaturezaProcedimentos;
                consentimentoExistente.MedicinaQuanticaPossiveisReacoes = MedicinaQuanticaPossiveisReacoes;
                consentimentoExistente.MedicinaQuanticaContraindicacoes = MedicinaQuanticaContraindicacoes;
                consentimentoExistente.MedicinaQuanticaLiberdadeInterromper = MedicinaQuanticaLiberdadeInterromper;
                consentimentoExistente.MedicinaQuanticaRevogavel = MedicinaQuanticaRevogavel;
                consentimentoExistente.MedicinaQuanticaAssinatura = MedicinaQuanticaAssinatura;
                consentimentoExistente.MedicinaQuanticaDataAssinatura = !string.IsNullOrEmpty(MedicinaQuanticaAssinatura) ? DateTime.Now : null;

                consentimentoExistente.RgpdInformacaoResponsavel = RgpdInformacaoResponsavel;
                consentimentoExistente.RgpdDireitosAcesso = RgpdDireitosAcesso;
                consentimentoExistente.RgpdConsentimentoExplicito = RgpdConsentimentoExplicito;
                consentimentoExistente.RgpdOpcaoNaoMarketing = RgpdOpcaoNaoMarketing;
                consentimentoExistente.RgpdRevogavel = RgpdRevogavel;
                consentimentoExistente.RgpdAssinatura = RgpdAssinatura;
                consentimentoExistente.RgpdDataAssinatura = !string.IsNullOrEmpty(RgpdAssinatura) ? DateTime.Now : null;

                consentimentoExistente.AssinaturaQuestionarioCompleto = AssinaturaQuestionarioCompleto;
                consentimentoExistente.DataAssinaturaQuestionario = !string.IsNullOrEmpty(AssinaturaQuestionarioCompleto) ? DateTime.Now : null;
                consentimentoExistente.QuestionarioAssinado = QuestionarioAssinado;

                consentimentoExistente.DataUltimaAtualizacao = DateTime.Now;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar consentimentos: {ex.Message}", ex);
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