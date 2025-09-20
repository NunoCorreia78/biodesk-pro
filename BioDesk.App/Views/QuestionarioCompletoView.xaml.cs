using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BioDesk.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BioDesk.App.Views
{
    public partial class QuestionarioCompletoView : UserControl
    {
        public QuestionarioCompletoView()
        {
            InitializeComponent();
            
            // Não definir DataContext aqui - será definido pelo parent control
            // DataContext será definido externamente quando necessário
        }

        private void WebBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                // Processar carregamento do WebBrowser
                System.Diagnostics.Debug.WriteLine("WebBrowser carregado");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro no LoadCompleted: {ex.Message}");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // As TextBoxes estão sempre visíveis agora, por isso este método pode ficar vazio
            // Mantemos o método para evitar erros de binding no XAML
        }

        private void WebBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            try
            {
                // Cancelar navegação para links externos
                if (e.Uri != null && !e.Uri.ToString().StartsWith("about:"))
                {
                    if (e.Uri.ToString().StartsWith("http"))
                    {
                        e.Cancel = true;
                        
                        // Abrir no navegador padrão
                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = e.Uri.ToString(),
                                UseShellExecute = true
                            });
                        }
                        catch
                        {
                            // Ignorar erro se não conseguir abrir
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro na navegação: {ex.Message}");
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("🔥🔥🔥 BOTÃO GUARDAR CLICADO! 🔥🔥🔥");
                System.Diagnostics.Debug.WriteLine($"DataContext: {this.DataContext?.GetType().Name}");
                
                if (this.DataContext is QuestionarioCompletoViewModel vm)
                {
                    System.Diagnostics.Debug.WriteLine($"ViewModel encontrado! PacienteId: {vm.PacienteId}");
                    System.Diagnostics.Debug.WriteLine($"GuardarCommand: {vm.GuardarCommand != null}");
                    
                    // Tentar executar o comando manualmente
                    if (vm.GuardarCommand != null && vm.GuardarCommand.CanExecute(null))
                    {
                        System.Diagnostics.Debug.WriteLine("Executando comando manualmente...");
                        vm.GuardarCommand.Execute(null);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("ERRO: Comando não pode ser executado!");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ERRO: DataContext não é QuestionarioCompletoViewModel!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERRO no TestButton_Click: {ex.Message}");
            }
        }
    }
}