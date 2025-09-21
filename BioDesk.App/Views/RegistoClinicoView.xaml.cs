using System.Windows.Controls;
using BioDesk.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BioDesk.App.Views
{
    public partial class RegistoClinicoView : UserControl
    {
        public RegistoClinicoView()
        {
            InitializeComponent();
            
            // Initialize ViewModel if needed
            if (DataContext == null)
            {
                try
                {
                    // Note: In a real application, this would be injected
                    // For now, we'll create a basic instance
                    // DataContext = App.ServiceProvider?.GetService<RegistoClinicoViewModel>();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error initializing RegistoClinicoView: {ex.Message}");
                }
            }
        }
        
        public void SetViewModel(RegistoClinicoViewModel viewModel)
        {
            DataContext = viewModel;
        }
    }
}