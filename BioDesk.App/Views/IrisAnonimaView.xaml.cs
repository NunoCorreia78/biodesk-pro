using System.Windows;
using System.Windows.Controls;
using BioDesk.App.ViewModels;

namespace BioDesk.App.Views;

public partial class IrisAnonimaView : UserControl
{
    public IrisAnonimaView()
    {
        InitializeComponent();
    }
    
    private void BackToHome_Click(object sender, RoutedEventArgs e)
    {
        // Voltar para Home (será melhorado com DI)
        if (Application.Current.MainWindow?.DataContext is MainWindowViewModel vm)
        {
            vm.NavigationService.GoTo("Home");
        }
    }
}