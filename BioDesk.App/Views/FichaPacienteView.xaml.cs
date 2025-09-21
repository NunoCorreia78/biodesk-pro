using System.Windows.Controls;
using System.Windows;
using BioDesk.App.ViewModels;
using BioDesk.App.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BioDesk.App.Views;

public partial class FichaPacienteView : UserControl
{
    private QuestionarioCompletoViewModel? _questionarioCompletoViewModel;
    private RegistoClinicoViewModel? _registoClinicoViewModel;
    
    public FichaPacienteView()
    {
        InitializeComponent();
        InitializeViewModels();
        this.Loaded += OnViewLoaded;
        this.DataContextChanged += OnDataContextChanged;
    }
    
    private void InitializeViewModels()
    {
        try
        {
            // Create navigation service (simple implementation for now)
            var navigationService = new NavigationService();
            var serviceProvider = App.Services ?? throw new InvalidOperationException("ServiceProvider not initialized");
            
            // Initialize QuestionarioCompletoViewModel
            _questionarioCompletoViewModel = new QuestionarioCompletoViewModel(navigationService, serviceProvider);
            
            // Initialize RegistoClinicoViewModel
            _registoClinicoViewModel = new RegistoClinicoViewModel(navigationService, serviceProvider);
            
            System.Diagnostics.Debug.WriteLine("ViewModels inicializados sem alterar DataContext");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao inicializar ViewModels: {ex.Message}");
        }
    }
    
    private void OnViewLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("FichaPacienteView.OnViewLoaded iniciado");
            
            // Configure the QuestionarioCompletoView DataContext after loading
            var questionarioView = this.FindName("QuestionarioView") as QuestionarioCompletoView;
            System.Diagnostics.Debug.WriteLine($"QuestionarioView encontrado: {questionarioView != null}");
            System.Diagnostics.Debug.WriteLine($"ViewModel disponível: {_questionarioCompletoViewModel != null}");
            
            if (questionarioView != null && _questionarioCompletoViewModel != null)
            {
                System.Diagnostics.Debug.WriteLine("Definindo DataContext do QuestionarioView...");
                questionarioView.DataContext = _questionarioCompletoViewModel;
                System.Diagnostics.Debug.WriteLine("DataContext definido com sucesso");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ERRO: Não foi possível definir DataContext do QuestionarioView");
                if (questionarioView == null) System.Diagnostics.Debug.WriteLine("QuestionarioView é null");
                if (_questionarioCompletoViewModel == null) System.Diagnostics.Debug.WriteLine("_questionarioCompletoViewModel é null");
            }

            // Configure the RegistoClinicoView DataContext after loading
            var registoClinicoView = this.FindName("RegistoClinicoView") as RegistoClinicoView;
            System.Diagnostics.Debug.WriteLine($"RegistoClinicoView encontrado: {registoClinicoView != null}");
            System.Diagnostics.Debug.WriteLine($"RegistoClinicoViewModel disponível: {_registoClinicoViewModel != null}");
            
            if (registoClinicoView != null && _registoClinicoViewModel != null)
            {
                System.Diagnostics.Debug.WriteLine("Definindo DataContext do RegistoClinicoView...");
                registoClinicoView.DataContext = _registoClinicoViewModel;
                System.Diagnostics.Debug.WriteLine("RegistoClinicoView DataContext definido com sucesso");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ERRO: Não foi possível definir DataContext do RegistoClinicoView");
                if (registoClinicoView == null) System.Diagnostics.Debug.WriteLine("RegistoClinicoView é null");
                if (_registoClinicoViewModel == null) System.Diagnostics.Debug.WriteLine("_registoClinicoViewModel é null");
            }
            
            // Sincronizar dados do paciente
            SincronizarDadosPaciente();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"ERRO CRÍTICO em OnViewLoaded: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Tipo de exceção: {ex.GetType().Name}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            
            // Re-throw para mostrar o erro ao usuário
            throw;
        }
    }
    
    private void SincronizarDadosPaciente()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("=== INICIANDO SINCRONIZAR DADOS PACIENTE ===");
            
            if (this.DataContext is FichaPacienteViewModel fichaPacienteViewModel)
            {
                System.Diagnostics.Debug.WriteLine("FichaPacienteViewModel encontrado, obtendo dados do paciente...");
                
                var pacienteId = fichaPacienteViewModel.GetPacienteId();
                var nomePaciente = fichaPacienteViewModel.NomePaciente;
                
                System.Diagnostics.Debug.WriteLine($"Dados obtidos: ID={pacienteId}, Nome={nomePaciente}");
                
                // Sincronizar com QuestionarioCompletoViewModel
                if (_questionarioCompletoViewModel != null)
                {
                    _questionarioCompletoViewModel.SetPaciente(pacienteId, nomePaciente);
                    System.Diagnostics.Debug.WriteLine($"QuestionarioViewModel sincronizado: ID={pacienteId}, Nome={nomePaciente}");
                }
                
                // Sincronizar com RegistoClinicoViewModel
                if (_registoClinicoViewModel != null)
                {
                    _registoClinicoViewModel.SetPaciente(pacienteId, nomePaciente);
                    System.Diagnostics.Debug.WriteLine($"RegistoClinicoViewModel sincronizado: ID={pacienteId}, Nome={nomePaciente}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ERRO: FichaPacienteViewModel não encontrado");
                System.Diagnostics.Debug.WriteLine($"DataContext é FichaPacienteViewModel: {this.DataContext is FichaPacienteViewModel}");
                System.Diagnostics.Debug.WriteLine($"DataContext type: {this.DataContext?.GetType().Name}");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao sincronizar dados do paciente: {ex.Message}");
        }
    }
    
    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("=== DATA CONTEXT CHANGED ===");
            
            // Remover listener anterior se existir
            if (e.OldValue is FichaPacienteViewModel oldVm)
            {
                oldVm.PacienteCarregado -= OnPacienteCarregado;
            }
            
            // Adicionar listener novo
            if (e.NewValue is FichaPacienteViewModel newVm)
            {
                System.Diagnostics.Debug.WriteLine("Novo FichaPacienteViewModel detectado, adicionando event listener");
                newVm.PacienteCarregado += OnPacienteCarregado;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro em OnDataContextChanged: {ex.Message}");
        }
    }
    
    private void OnPacienteCarregado(object? sender, EventArgs e)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("=== EVENTO PACIENTE CARREGADO ===");
            SincronizarDadosPaciente();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro em OnPacienteCarregado: {ex.Message}");
        }
    }

    public QuestionarioCompletoViewModel? QuestionarioCompletoViewModel => _questionarioCompletoViewModel;
}