using System.Windows;
using System.Windows.Controls;

namespace BioDesk.App.Controls;

/// <summary>
/// Header transversal para toda a aplicação
/// </summary>
public partial class AppHeader : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(AppHeader),
            new PropertyMetadata("BioDesk PRO"));

    public static readonly DependencyProperty SubtitleProperty =
        DependencyProperty.Register(nameof(Subtitle), typeof(string), typeof(AppHeader),
            new PropertyMetadata("Gestão clínica — Naturopatia · Osteopatia · Medicina Quântica"));

    public static readonly DependencyProperty RightContentProperty =
        DependencyProperty.Register(nameof(RightContent), typeof(object), typeof(AppHeader),
            new PropertyMetadata(null));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public object RightContent
    {
        get => GetValue(RightContentProperty);
        set => SetValue(RightContentProperty, value);
    }

    public AppHeader()
    {
        InitializeComponent();
    }
}