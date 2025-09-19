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
    
    [ObservableProperty]
    private Paciente? pacienteSelecionado;
    
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
    
    private async Task CarregarPacientesAsync()
    {
        try
        {
            IsLoading = true;
            
            // Garantir que a base de dados existe
            await _context.Database.EnsureCreatedAsync();
            
            var pacientes = await _context.Pacientes
                .Where(p => p.Ativo)
                .OrderBy(p => p.PrimeiroNome)
                .ThenBy(p => p.UltimoNome)
                .ToListAsync();
            
            Pacientes.Clear();
            PacientesFiltrados.Clear();
            
            foreach (var paciente in pacientes)
            {
                Pacientes.Add(paciente);
                PacientesFiltrados.Add(paciente);
            }
        }
        catch (Exception ex)
        {
            // Log error - por agora só mostra na consola
            System.Diagnostics.Debug.WriteLine($"Erro ao carregar pacientes: {ex.Message}");
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
            (p.NumeroUtente?.Contains(termo) ?? false)
        );
        
        foreach (var paciente in pacientesFiltrados)
        {
            PacientesFiltrados.Add(paciente);
        }
    }
    
    [RelayCommand]
    private async Task NovoPaciente()
    {
        // Por agora, apenas mostra uma mensagem
        System.Diagnostics.Debug.WriteLine("Novo paciente - funcionalidade em desenvolvimento");
        
        // TODO: Abrir janela de criação de paciente
        await Task.CompletedTask;
    }
    
    [RelayCommand]
    private async Task EditarPaciente()
    {
        if (PacienteSelecionado == null) return;
        
        // Por agora, apenas mostra uma mensagem
        System.Diagnostics.Debug.WriteLine($"Editar paciente: {PacienteSelecionado.NomeCompleto}");
        
        // TODO: Abrir janela de edição de paciente
        await Task.CompletedTask;
    }
    
    [RelayCommand]
    private async Task EliminarPaciente()
    {
        if (PacienteSelecionado == null) return;
        
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
    
    [RelayCommand]
    private void Voltar()
    {
        _navigationService.NavigateToHome();
    }
}