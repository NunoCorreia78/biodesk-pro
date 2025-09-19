using System.Windows;

namespace BioDesk.App;

public partial class SimpleApp : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            base.OnStartup(e);
            
            var window = new Window
            {
                Title = "BioDesk PRO - Teste Simples",
                Width = 400,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            
            window.Content = new System.Windows.Controls.TextBlock
            {
                Text = "BioDesk PRO\nTeste de funcionamento básico",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16
            };
            
            window.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro no teste simples: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}", 
                           "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}