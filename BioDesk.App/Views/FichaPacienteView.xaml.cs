using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using BioDesk.App.Data;
using BioDesk.App.ViewModels;
using BioDesk.App.Models;
using BioDesk.App.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BioDesk.App.Views;

public partial class FichaPacienteView : UserControl
{
    private QuestionarioSaudeViewModel? _questionarioViewModel;
    
    public FichaPacienteView()
    {
        InitializeXamlComponent();
        InitializeQuestionarioViewModel();
    }
    
    private void InitializeXamlComponent()
    {
        try
        {
            // Try to call InitializeComponent using reflection
            var method = this.GetType().GetMethod("InitializeComponent", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (method != null)
            {
                method.Invoke(this, null);
            }
        }
        catch
        {
            // Fallback if reflection fails - manual initialization would go here
        }
    }
    
    private void InitializeQuestionarioViewModel()
    {
        try
        {
            // Create a new DbContext instance directly for now
            // In the future, this should be properly injected
            var optionsBuilder = new DbContextOptionsBuilder<BioDeskDbContext>();
            var dbContext = new BioDeskDbContext(optionsBuilder.Options);
            _questionarioViewModel = new QuestionarioSaudeViewModel(dbContext);
                    
            // Bind the ViewModel to the questionnaire section
            // We'll need to find the questionnaire controls and set their DataContext
            this.Loaded += OnViewLoaded;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao inicializar QuestionarioSaudeViewModel: {ex.Message}");
        }
    }
    
    private void OnViewLoaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (_questionarioViewModel != null)
        {
            // Set DataContext for questionnaire controls
            SetQuestionarioDataContext();
        }
    }
    
    private void SetQuestionarioDataContext()
    {
        try
        {
            // Definir DataContext para o painel principal
            if (this.FindName("QuestionarioConsentimentosPanel") is StackPanel panel)
            {
                panel.DataContext = _questionarioViewModel;
            }
            
            // Configurar DataContext para controles individuais (fallback)
            // Find questionnaire controls by name and set their DataContext
            if (this.FindName("txt_DoencaCronica") is TextBox doencaCronica)
                doencaCronica.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_SintomasAtuais") is TextBox sintomasAtuais)
                sintomasAtuais.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_MedicacaoAtual") is TextBox medicacaoAtual)
                medicacaoAtual.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_AlergiasAlimentares") is TextBox alergiasAlimentares)
                alergiasAlimentares.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_AlergiasMedicamentos") is TextBox alergiasMedicamentos)
                alergiasMedicamentos.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_AlergiasAmbientais") is TextBox alergiasAmbientais)
                alergiasAmbientais.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_AlergiasPlantasSupl") is TextBox alergiasPlantasSupl)
                alergiasPlantasSupl.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_HistoricoCirurgico") is TextBox historicoCirurgico)
                historicoCirurgico.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_Fraturas") is TextBox fraturas)
                fraturas.DataContext = _questionarioViewModel;
                
            if (this.FindName("cb_AtividadeFisica") is ComboBox atividadeFisica)
                atividadeFisica.DataContext = _questionarioViewModel;
                
            if (this.FindName("cb_Tabagismo") is ComboBox tabagismo)
                tabagismo.DataContext = _questionarioViewModel;
                
            if (this.FindName("cb_ConsumoAlcool") is ComboBox consumoAlcool)
                consumoAlcool.DataContext = _questionarioViewModel;
                
            if (this.FindName("txt_HabitosAlimentares") is TextBox habitosAlimentares)
                habitosAlimentares.DataContext = _questionarioViewModel;
                
            // Configurar DataContext para os botões de ação
            if (this.FindName("GuardarQuestionarioButton") is Button guardarButton)
                guardarButton.DataContext = _questionarioViewModel;
                
            if (this.FindName("GerarConsentimentoButton") is Button consentimentoButton)
                consentimentoButton.DataContext = _questionarioViewModel;
                
            // Se não encontrar pelos nomes, tentar encontrar por tipo na árvore visual
            ConfigurarButtonsDataContext();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao configurar DataContext dos controles: {ex.Message}");
        }
    }
    
    private void ConfigurarButtonsDataContext()
    {
        try
        {
            // Encontrar todos os botões e configurar DataContext para aqueles com Commands
            var buttons = FindVisualChildren<Button>(this);
            foreach (var button in buttons)
            {
                if (button.Command != null)
                {
                    button.DataContext = _questionarioViewModel;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao configurar DataContext dos botões: {ex.Message}");
        }
    }
    
    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
    {
        if (parent == null) yield break;
        
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T typedChild)
                yield return typedChild;
                
            foreach (var descendant in FindVisualChildren<T>(child))
                yield return descendant;
        }
    }
    
    public void SetPacienteSelecionado(Paciente? paciente)
    {
        _questionarioViewModel?.SetPaciente(paciente);
    }
    
    private void AbrirAssinatura_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (_questionarioViewModel == null) return;

        try
        {
            // Criar e mostrar janela de assinatura
            var assinaturaWindow = new AssinaturaWindow();
            assinaturaWindow.Owner = Window.GetWindow(this);
            assinaturaWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            
            // Configurar eventos
            assinaturaWindow.AssinaturaCompleted += async (s, args) =>
            {
                try
                {
                    await _questionarioViewModel.ProcessarAssinaturaAsync(
                        args.SignatureData,
                        args.SignatureImage!,
                        args.StrokeCount,
                        args.Bounds.Width,
                        args.Bounds.Height);
                    
                    assinaturaWindow.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao processar assinatura: {ex.Message}", 
                                   "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            assinaturaWindow.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao abrir janela de assinatura: {ex.Message}", 
                           "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}