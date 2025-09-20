using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using BioDesk.App.Services;
using BioDesk.App.ViewModels;
using BioDesk.App.Views;
using BioDesk.App.Data;

namespace BioDesk.App;

public partial class MainWindow : Window
{
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;
    private ContentControl? _contentArea;

    public MainWindow(INavigationService navigationService, IServiceProvider serviceProvider)
    {
        // Initialize XAML component - use reflection to avoid IntelliSense issues
        InitializeXamlComponent();
        
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        
        // Get reference to ContentArea after InitializeComponent
        _contentArea = FindName("ContentArea") as ContentControl;
        
        // Criar ViewModel manualmente para evitar problemas de DI
        DataContext = new MainWindowViewModel(_navigationService);
        
        // Subscribe to navigation changes
        _navigationService.PropertyChanged += OnNavigationChanged;
        
        // Load initial view
        LoadView("Home");
        
        // Setup keyboard shortcuts
        SetupKeyboardShortcuts();
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
                return;
            }
        }
        catch
        {
            // If reflection fails, use manual initialization
        }
        
        // Fallback to manual initialization
        InitializeComponentManually();
    }

    private void InitializeComponentManually()
    {
        // Fallback manual initialization if needed
        this.Title = "BioDesk PRO";
        this.Height = 720;
        this.Width = 1280;
        this.MinHeight = 600;
        this.MinWidth = 800;
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        // Create the main grid and content area
        var grid = new Grid();
        _contentArea = new ContentControl { Name = "ContentArea" };
        grid.Children.Add(_contentArea);
        this.Content = grid;
    }

    private void OnNavigationChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(INavigationService.CurrentView))
        {
            LoadView(_navigationService.CurrentView);
        }
    }

    private void LoadView(string viewName)
    {
        try
        {
            UserControl? view = viewName switch
            {
                "Home" => CreateHomeView(),
                "Pacientes" => CreatePacientesView(),
                "IrisAnonima" => new IrisAnonimaView(),
                "FichaPaciente" => CreateFichaPacienteView(),
                "QuestionarioCompleto" => CreateQuestionarioCompletoView(),
                _ => CreateHomeView()
            };

            if (_contentArea != null && view != null)
            {
                _contentArea.Content = view;
            }
        }
        catch (Exception ex)
        {
            // Fallback em caso de erro
            var errorView = new System.Windows.Controls.TextBlock
            {
                Text = $"Erro ao carregar vista: {ex.Message}",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            
            if (_contentArea != null)
            {
                _contentArea.Content = errorView;
            }
        }
    }

    private HomeView CreateHomeView()
    {
        try
        {
            var homeView = new HomeView();
            var homeViewModel = new HomeViewModel(_navigationService);
            homeView.DataContext = homeViewModel;
            return homeView;
        }
        catch
        {
            // Fallback se DI falhar
            var homeView = new HomeView();
            homeView.DataContext = new HomeViewModel(_navigationService);
            return homeView;
        }
    }

    private PacientesView CreatePacientesView()
    {
        try
        {
            var pacientesView = new PacientesView();
            var pacientesViewModel = _serviceProvider.GetRequiredService<PacientesViewModel>();
            pacientesView.DataContext = pacientesViewModel;
            
            // Recarregar dados sempre que a vista for criada
            _ = pacientesViewModel.CarregarPacientesAsync();
            
            return pacientesView;
        }
        catch
        {
            // Fallback se DI falhar - criar uma instância simples
            var pacientesView = new PacientesView();
            // O PacientesViewModel precisa do DbContext, então vamos usar DI
            var dbContext = _serviceProvider.GetRequiredService<BioDeskDbContext>();
            var navigationService = (NavigationService)_navigationService;
            var viewModel = new PacientesViewModel(dbContext, navigationService);
            pacientesView.DataContext = viewModel;
            
            // Recarregar dados
            _ = viewModel.CarregarPacientesAsync();
            
            return pacientesView;
        }
    }

    private FichaPacienteView CreateFichaPacienteView()
    {
        try
        {
            var fichaPacienteView = new FichaPacienteView();
            var dbContext = _serviceProvider.GetRequiredService<BioDeskDbContext>();
            var navigationService = (NavigationService)_navigationService;
            var viewModel = new FichaPacienteViewModel(dbContext, navigationService);
            
            // Carregar dados do paciente se foi passado um ID
            if (_navigationService.NavigationParameter is int pacienteId)
            {
                _ = Task.Run(async () =>
                {
                    await viewModel.CarregarPacienteAsync(pacienteId);
                });
            }
            
            fichaPacienteView.DataContext = viewModel;
            
            return fichaPacienteView;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao criar FichaPacienteView: {ex.Message}");
            // Fallback
            var fichaPacienteView = new FichaPacienteView();
            var dbContext = _serviceProvider.GetRequiredService<BioDeskDbContext>();
            var navigationService = (NavigationService)_navigationService;
            fichaPacienteView.DataContext = new FichaPacienteViewModel(dbContext, navigationService);
            return fichaPacienteView;
        }
    }

    private QuestionarioCompletoView CreateQuestionarioCompletoView()
    {
        try
        {
            // Usar versão simplificada por enquanto
            var questionarioView = new QuestionarioCompletoView();
            
            // O ViewModel já é criado dentro da View
            return questionarioView;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao criar QuestionarioCompletoView: {ex.Message}");
            
            // Fallback - criar view básica
            var fallbackView = new QuestionarioCompletoView();
            return fallbackView;
        }
    }

    private void SetupKeyboardShortcuts()
    {
        // Alt+1 para Pacientes
        var pacientesGesture = new KeyGesture(Key.D1, ModifierKeys.Alt);
        var pacientesCommand = new RoutedCommand();
        CommandBindings.Add(new CommandBinding(pacientesCommand, (s, e) => _navigationService.GoTo("Pacientes")));
        InputBindings.Add(new KeyBinding(pacientesCommand, pacientesGesture));

        // Alt+2 para Íris Anónima
        var irisGesture = new KeyGesture(Key.D2, ModifierKeys.Alt);
        var irisCommand = new RoutedCommand();
        CommandBindings.Add(new CommandBinding(irisCommand, (s, e) => _navigationService.GoTo("IrisAnonima")));
        InputBindings.Add(new KeyBinding(irisCommand, irisGesture));
    }

    protected override void OnClosed(EventArgs e)
    {
        _navigationService.PropertyChanged -= OnNavigationChanged;
        base.OnClosed(e);
    }
}