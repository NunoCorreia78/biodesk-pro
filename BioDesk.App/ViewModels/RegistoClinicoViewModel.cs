using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BioDesk.App.Data;
using BioDesk.App.Models;
using BioDesk.App.Services;
using BioDesk.App.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BioDesk.App.ViewModels
{
    public partial class RegistoClinicoViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private int _pacienteId;

        public RegistoClinicoViewModel(INavigationService navigationService, IServiceProvider serviceProvider)
        {
            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
            InitializeCommands();
            InitializeCollections();
        }

        #region Properties

        public int PacienteId
        {
            get => _pacienteId;
            set => SetProperty(ref _pacienteId, value);
        }

        // Identificação do Paciente
        private Paciente? _pacienteSelecionado;
        public Paciente? PacienteSelecionado
        {
            get => _pacienteSelecionado;
            set => SetProperty(ref _pacienteSelecionado, value);
        }

        // Motivo da Consulta
        private string _motivoConsulta = string.Empty;
        public string MotivoConsulta
        {
            get => _motivoConsulta;
            set => SetProperty(ref _motivoConsulta, value);
        }

        // História Clínica
        private string _historiaClinica = string.Empty;
        public string HistoriaClinica
        {
            get => _historiaClinica;
            set => SetProperty(ref _historiaClinica, value);
        }

        // Exame Objetivo
        private string _exameObjetivo = string.Empty;
        public string ExameObjetivo
        {
            get => _exameObjetivo;
            set => SetProperty(ref _exameObjetivo, value);
        }

        private string _observacaoExame = string.Empty;
        public string ObservacaoExame
        {
            get => _observacaoExame;
            set => SetProperty(ref _observacaoExame, value);
        }

        private string _palpacaoExame = string.Empty;
        public string PalpacaoExame
        {
            get => _palpacaoExame;
            set => SetProperty(ref _palpacaoExame, value);
        }

        private string _mobilidadeExame = string.Empty;
        public string MobilidadeExame
        {
            get => _mobilidadeExame;
            set => SetProperty(ref _mobilidadeExame, value);
        }

        // Diagnóstico/Impressão Clínica
        private string _diagnostico = string.Empty;
        public string Diagnostico
        {
            get => _diagnostico;
            set => SetProperty(ref _diagnostico, value);
        }

        private string _categoriasDiagnostico = string.Empty;
        public string CategoriasDiagnostico
        {
            get => _categoriasDiagnostico;
            set => SetProperty(ref _categoriasDiagnostico, value);
        }

        // Collections
        private ObservableCollection<PlanoTerapeutico> _planoTerapeutico = new();
        public ObservableCollection<PlanoTerapeutico> PlanoTerapeutico
        {
            get => _planoTerapeutico;
            set => SetProperty(ref _planoTerapeutico, value);
        }

        private ObservableCollection<NotaEvolucao> _notasEvolucao = new();
        public ObservableCollection<NotaEvolucao> NotasEvolucao
        {
            get => _notasEvolucao;
            set => SetProperty(ref _notasEvolucao, value);
        }

        private ObservableCollection<string> _motivosConsulta = new();
        public ObservableCollection<string> MotivosConsulta
        {
            get => _motivosConsulta;
            set => SetProperty(ref _motivosConsulta, value);
        }

        // Resumo na coluna direita
        private DateTime? _ultimaConsulta;
        public DateTime? UltimaConsulta
        {
            get => _ultimaConsulta;
            set => SetProperty(ref _ultimaConsulta, value);
        }

        private string _resumoUltimaConsulta = string.Empty;
        public string ResumoUltimaConsulta
        {
            get => _resumoUltimaConsulta;
            set => SetProperty(ref _resumoUltimaConsulta, value);
        }

        private string _alertasImportantes = string.Empty;
        public string AlertasImportantes
        {
            get => _alertasImportantes;
            set => SetProperty(ref _alertasImportantes, value);
        }

        private string _textoPesquisa = string.Empty;
        public string TextoPesquisa
        {
            get => _textoPesquisa;
            set => SetProperty(ref _textoPesquisa, value);
        }

        // Controle de estado
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _temAlteracoes;
        public bool TemAlteracoes
        {
            get => _temAlteracoes;
            set => SetProperty(ref _temAlteracoes, value);
        }

        #endregion

        #region Commands

        [RelayCommand]
        private async Task EditarPaciente()
        {
            // TODO: Implementar edição de dados do paciente
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task AdicionarAntecedente()
        {
            // TODO: Implementar adição de antecedente
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task AnexarImagem()
        {
            // TODO: Implementar anexo de imagem
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task GerarPrescricao()
        {
            // TODO: Implementar geração de prescrição
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task GerarPdf()
        {
            // TODO: Implementar geração de PDF
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task ExportarFicha()
        {
            // TODO: Implementar exportação da ficha completa
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task EnviarEmail()
        {
            // TODO: Implementar envio por email
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task EnviarWhatsApp()
        {
            // TODO: Implementar envio por WhatsApp
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task AdicionarNotaEvolucao()
        {
            var novaNota = new NotaEvolucao
            {
                Data = DateTime.Now,
                Nota = "Nova nota de evolução...",
                ProfissionalId = 1 // TODO: Obter do contexto
            };
            
            NotasEvolucao.Add(novaNota);
            TemAlteracoes = true;
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task AdicionarTerapia()
        {
            var novaTerapia = new PlanoTerapeutico
            {
                Terapia = "Nova terapia",
                Data = DateTime.Now,
                Frequencia = "Conforme necessário"
            };
            
            PlanoTerapeutico.Add(novaTerapia);
            TemAlteracoes = true;
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task Salvar()
        {
            try
            {
                IsLoading = true;
                
                // TODO: Implementar lógica de salvamento
                await Task.Delay(1000); // Simular salvamento
                
                TemAlteracoes = false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        #endregion

        #region Private Methods

        private void InitializeCommands()
        {
            // Commands já são inicializados automaticamente pelo RelayCommand
        }

        private void InitializeCollections()
        {
            // Inicializar motivos de consulta comuns
            MotivosConsulta = new ObservableCollection<string>
            {
                "Dor lombar",
                "Check-up",
                "Acompanhamento",
                "Dor cervical",
                "Stress",
                "Ansiedade",
                "Problemas digestivos",
                "Consulta de rotina",
                "Primeira consulta",
                "Seguimento terapêutico"
            };

            // Inicializar exemplo de notas de evolução
            NotasEvolucao = new ObservableCollection<NotaEvolucao>
            {
                new NotaEvolucao
                {
                    Id = 1,
                    Data = DateTime.Now.AddDays(-7),
                    Nota = "Paciente refere melhoria dos sintomas após tratamento iniciado.",
                    ProfissionalId = 1
                },
                new NotaEvolucao
                {
                    Id = 2,
                    Data = DateTime.Now.AddDays(-14),
                    Nota = "Primeira consulta. Paciente apresenta quadro de dor lombar crónica.",
                    ProfissionalId = 1
                }
            };

            // Inicializar exemplo de plano terapêutico
            PlanoTerapeutico = new ObservableCollection<PlanoTerapeutico>
            {
                new PlanoTerapeutico
                {
                    Id = 1,
                    Terapia = "Osteopatia",
                    Data = DateTime.Now,
                    Frequencia = "Semanal"
                },
                new PlanoTerapeutico
                {
                    Id = 2,
                    Terapia = "Fitoterapia - Curcuma",
                    Data = DateTime.Now,
                    Frequencia = "2x dia"
                }
            };
        }

        public void SetPaciente(int pacienteId, string nomePaciente)
        {
            PacienteId = pacienteId;
            // TODO: Carregar dados completos do paciente
            PacienteSelecionado = new Paciente 
            { 
                Id = pacienteId, 
                NomeCompleto = nomePaciente 
            };

            // Carregar dados do registo clínico
            CarregarDadosClínicos();
        }

        private async void CarregarDadosClínicos()
        {
            try
            {
                IsLoading = true;
                
                // TODO: Implementar carregamento real dos dados
                await Task.Delay(500); // Simular carregamento
                
                // Dados de exemplo
                UltimaConsulta = DateTime.Now.AddDays(-7);
                ResumoUltimaConsulta = "Tratamento para dor lombar em curso. Paciente refere melhoria.";
                AlertasImportantes = "⚠️ Alergia: Penicilina\n⚠️ Hipertensão controlada";
            }
            finally
            {
                IsLoading = false;
            }
        }

        #endregion
    }

    // Classes auxiliares (temporárias - devem ser movidas para Models)
    public class PlanoTerapeutico
    {
        public int Id { get; set; }
        public string Terapia { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Frequencia { get; set; } = string.Empty;
    }

    public class NotaEvolucao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Nota { get; set; } = string.Empty;
        public int ProfissionalId { get; set; }
    }
}