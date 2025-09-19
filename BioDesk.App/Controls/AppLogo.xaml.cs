using System.Windows;
using System.Windows.Controls;

namespace BioDesk.App.Controls;

/// <summary>
/// Controlo de logo transversal para toda a aplicação
/// </summary>
public partial class AppLogo : UserControl
{
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(double), typeof(AppLogo),
            new PropertyMetadata(56.0));

    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public AppLogo()
    {
        InitializeXamlComponent();
    }

    private void InitializeXamlComponent()
    {
        try
        {
            // Try to call InitializeComponent using reflection
            var method = this.GetType().GetMethod("InitializeComponent", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            method?.Invoke(this, null);
        }
        catch
        {
            // If reflection fails, continue anyway
        }
    }
}