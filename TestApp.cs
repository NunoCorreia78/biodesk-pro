using System;
using System.Windows;

namespace BioDesk.App
{
    public partial class TestApp : Application
    {
        [STAThread]
        public static void Main()
        {
            try
            {
                var app = new TestApp();
                
                var window = new Window
                {
                    Title = "BioDesk PRO - Teste",
                    Width = 800,
                    Height = 600,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                
                window.Content = new System.Windows.Controls.TextBlock
                {
                    Text = "BioDesk PRO está a funcionar!\nSe vê esta mensagem, o problema estava na inicialização complexa.",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 16
                };
                
                app.Run(window);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}", "Erro Crítico", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}