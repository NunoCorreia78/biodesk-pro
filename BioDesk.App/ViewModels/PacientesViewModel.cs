using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using BioDesk.App.Models;
using BioDesk.App.Data;
using BioDesk.App.Services;
using BioDesk.App.ViewModels.Base;

namespace BioDesk.App.ViewModels;

public partial class PacientesViewModel : ViewModelBase
{
    private readonly BioDeskDbContext _context;
    private readonly NavigationService _navigationService;
    
    [ObservableProperty]
    private ObservableCollection<Paciente> pacientes = new();
    
    [ObservableProperty]
    private ObservableCollection<Paciente> pacientesFiltrados = new();
    
    private Paciente? _pacienteSelecionado;
    public Paciente? PacienteSelecionado 
    { 
        get => _pacienteSelecionado;
        set 
        {
            if (SetProperty(ref _pacienteSelecionado, value))
            {
                // Notificar comandos para reavaliar CanExecute
                AbrirFichaCommand.NotifyCanExecuteChanged();
                EliminarPacienteCommand.NotifyCanExecuteChanged();
                BioDesk.App.App.DebugLog($"Paciente selecionado mudou para: {value?.NomeCompleto ?? "NULL"}");
            }
        }
    }
    
    [ObservableProperty]
    private string pesquisaTexto = string.Empty;
    
    [ObservableProperty]
    private bool isLoading;
    
    public PacientesViewModel(BioDeskDbContext context, NavigationService navigationService)
    {
        _context = context;
        _navigationService = navigationService;
        
        PropertyChanged += OnPropertyChanged;
        
        _ = CarregarPacientesAsync();
    }
    
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PesquisaTexto))
        {
            FiltrarPacientes();
        }
    }
    
    public async Task CarregarPacientesAsync()
    {
        try
        {
            BioDesk.App.App.DebugLog("=== CARREGANDO PACIENTES ===");
            IsLoading = true;
            
            // Garantir que a base de dados existe
            await _context.Database.EnsureCreatedAsync();
            BioDesk.App.App.DebugLog("Base de dados verificada/criada");
            
            var pacientes = await _context.Pacientes
                .Where(p => p.Ativo)
                .OrderBy(p => p.NomeCompleto)
                .ToListAsync();
                
            BioDesk.App.App.DebugLog($"Pacientes encontrados na BD: {pacientes.Count}");
            
            Pacientes.Clear();
            PacientesFiltrados.Clear();
            
            foreach (var paciente in pacientes)
            {
                Pacientes.Add(paciente);
                PacientesFiltrados.Add(paciente);
            }
            
            BioDesk.App.App.DebugLog($"Total na ObservableCollection: {Pacientes.Count}");
        }
        catch (Exception ex)
        {
            BioDesk.App.App.DebugLog($"=== ERRO AO CARREGAR PACIENTES ===");
            BioDesk.App.App.DebugLog($"Erro: {ex.Message}");
            BioDesk.App.App.DebugLog($"StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                BioDesk.App.App.DebugLog($"InnerException: {ex.InnerException.Message}");
            }
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    private void FiltrarPacientes()
    {
        PacientesFiltrados.Clear();
        
        if (string.IsNullOrWhiteSpace(PesquisaTexto))
        {
            foreach (var paciente in Pacientes)
            {
                PacientesFiltrados.Add(paciente);
            }
            return;
        }
        
        var termo = PesquisaTexto.ToLowerInvariant();
        var pacientesFiltrados = Pacientes.Where(p =>
            p.NomeCompleto.ToLowerInvariant().Contains(termo) ||
            (p.Email?.ToLowerInvariant().Contains(termo) ?? false) ||
            (p.Telefone?.Contains(termo) ?? false) ||
            (p.NIF?.Contains(termo) ?? false)
        );
        
        foreach (var paciente in pacientesFiltrados)
        {
            PacientesFiltrados.Add(paciente);
        }
    }
    
    [RelayCommand]
    private void NovoPaciente()
    {
        System.Diagnostics.Debug.WriteLine("*** COMANDO NOVO PACIENTE EXECUTADO ***");
        try
        {
            // Navegar para ficha de novo paciente (ID = 0)
            System.Diagnostics.Debug.WriteLine($"Navegando para FichaPaciente com ID = 0");
            _navigationService.NavigateToFichaPaciente(0);
            System.Diagnostics.Debug.WriteLine("Navegação executada com sucesso");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro na navegação: {ex.Message}");
        }
    }
    
    [RelayCommand]
    private void EditarPaciente()
    {
        if (PacienteSelecionado == null) return;
        
        // Navegar para ficha do paciente selecionado
        _navigationService.NavigateToFichaPaciente(PacienteSelecionado.Id);
    }
    
    [RelayCommand(CanExecute = nameof(CanAbrirFicha))]
    private void AbrirFicha()
    {
        BioDesk.App.App.DebugLog($"=== COMANDO ABRIR FICHA EXECUTADO ===");
        BioDesk.App.App.DebugLog($"PacienteSelecionado: {PacienteSelecionado?.NomeCompleto ?? "NULL"}");
        
        if (PacienteSelecionado == null) 
        {
            BioDesk.App.App.DebugLog("ERRO: Nenhum paciente selecionado");
            return;
        }
        
        BioDesk.App.App.DebugLog($"Navegando para ficha do paciente ID: {PacienteSelecionado.Id}");
        
        // Navegar para ficha do paciente selecionado (mesmo que editar)
        _navigationService.NavigateToFichaPaciente(PacienteSelecionado.Id);
    }

    private bool CanAbrirFicha() => PacienteSelecionado != null;
    
    [RelayCommand(CanExecute = nameof(CanEliminarPaciente))]
    private async Task EliminarPaciente()
    {
        BioDesk.App.App.DebugLog($"=== COMANDO ELIMINAR PACIENTE EXECUTADO ===");
        BioDesk.App.App.DebugLog($"PacienteSelecionado: {PacienteSelecionado?.NomeCompleto ?? "NULL"}");
        
        if (PacienteSelecionado == null) 
        {
            BioDesk.App.App.DebugLog("ERRO: Nenhum paciente selecionado para eliminar");
            return;
        }

        BioDesk.App.App.DebugLog($"Eliminando paciente ID: {PacienteSelecionado.Id}");
        
        try
        {
            // Soft delete - marca como inativo
            PacienteSelecionado.Ativo = false;
            
            _context.Pacientes.Update(PacienteSelecionado);
            await _context.SaveChangesAsync();
            
            // Remove da lista local
            Pacientes.Remove(PacienteSelecionado);
            PacientesFiltrados.Remove(PacienteSelecionado);
            
            PacienteSelecionado = null;
            
            System.Diagnostics.Debug.WriteLine("Paciente eliminado com sucesso");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao eliminar paciente: {ex.Message}");
        }
    }

    private bool CanEliminarPaciente() => PacienteSelecionado != null;
    
    [RelayCommand]
    private void Voltar()
    {
        _navigationService.NavigateToHome();
    }
}