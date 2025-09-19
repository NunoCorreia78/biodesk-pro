using System.Windows;
using System.Windows.Controls;
using BioDesk.App.Services;
using BioDesk.App.ViewModels;

namespace BioDesk.App.Views;

public partial class PacientesView : UserControl
{
    public PacientesView()
    {
        InitializeXamlComponent();
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
    
    private void BackToHome_Click(object sender, RoutedEventArgs e)
    {
        // O comando agora está no ViewModel
        if (DataContext is PacientesViewModel vm)
        {
            vm.VoltarCommand.Execute(null);
        }
    }
}