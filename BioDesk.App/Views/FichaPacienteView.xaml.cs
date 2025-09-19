using System.Windows.Controls;

namespace BioDesk.App.Views;

public partial class FichaPacienteView : UserControl
{
    public FichaPacienteView()
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
}