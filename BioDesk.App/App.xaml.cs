using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BioDesk.App.Services;
using BioDesk.App.ViewModels;
using BioDesk.App.Views;
using BioDesk.App.Data;

namespace BioDesk.App;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    public static void DebugLog(string message)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var logMessage = $"[{timestamp}] {message}";
        
        // Escrever no console
        Console.WriteLine(logMessage);
        
        // Escrever no ficheiro
        try
        {
            var logPath = Path.Combine(AppContext.BaseDirectory, "debug.log");
            File.AppendAllText(logPath, logMessage + "\n");
        }
        catch { /* Ignorar erros de escrita */ }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            // Iniciar logging simples
            DebugLog("=== BIODESK PRO DEBUG INICIADO (SEM CONSOLE) ===");
            DebugLog($"Diretório da aplicação: {AppContext.BaseDirectory}");
            
            base.OnStartup(e);
            
            ConfigureExceptionHandling();
            ConfigureServices();
            
            // Criar MainWindow diretamente sem DI complexo
            var navigationService = _serviceProvider!.GetRequiredService<INavigationService>();
            var mainWindow = new MainWindow(navigationService, _serviceProvider!);
            
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            // Log detalhado do erro de startup
            var errorMessage = $"🏥 BioDesk PRO - Erro durante startup:\n\n" +
                             $"Mensagem: {ex.Message}\n\n" +
                             $"StackTrace:\n{ex.StackTrace}\n\n" +
                             $"InnerException: {ex.InnerException?.Message}";
            
            MessageBox.Show(errorMessage, "🏥 BioDesk PRO - Erro de Inicialização", MessageBoxButton.OK, MessageBoxImage.Error);
            
            // Tentar escrever no log também
            try
            {
                var logsPath = Path.Combine(AppContext.BaseDirectory, "logs");
                Directory.CreateDirectory(logsPath);
                var logFile = Path.Combine(logsPath, "startup_error.log");
                File.WriteAllText(logFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] STARTUP ERROR:\n{errorMessage}\n\n");
            }
            catch { /* Ignore if can't write log */ }
            
            Shutdown(1);
        }
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();
        
        // Configurar DbContext (configuração definida no BioDeskDbContext.cs)
        services.AddDbContext<BioDeskDbContext>();
        
        // Registar serviços
        services.AddSingleton<INavigationService, NavigationService>();
        
        // Registar ViewModels
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<PacientesViewModel>();
        
        // Registar Views
        services.AddTransient<HomeView>();
        services.AddTransient<PacientesView>();
        services.AddTransient<IrisAnonimaView>();
        services.AddTransient<QuestionarioCompletoView>();
        
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureExceptionHandling()
    {
        // Global exception handling
        DispatcherUnhandledException += OnDispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
    }

    private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        var errorDetails = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] DISPATCHER ERROR:\n" +
                          $"Mensagem: {e.Exception.Message}\n" +
                          $"Tipo: {e.Exception.GetType().Name}\n" +
                          $"StackTrace:\n{e.Exception.StackTrace}\n";
        
        if (e.Exception.InnerException != null)
        {
            errorDetails += $"\nInnerException: {e.Exception.InnerException.Message}\n" +
                           $"InnerStackTrace:\n{e.Exception.InnerException.StackTrace}\n";
        }
        
        LogException(e.Exception, "DISPATCHER_ERROR");
        
        MessageBox.Show(
            $"Erro inesperado detectado:\n\n{e.Exception.Message}\n\n" +
            "Detalhes completos foram guardados no ficheiro de log.\n\n" +
            $"Tipo do erro: {e.Exception.GetType().Name}",
            "Erro Fatal - Debug",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
        
        e.Handled = true;
        Shutdown();
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception exception)
        {
            LogException(exception, "UNHANDLED_EXCEPTION");
            
            MessageBox.Show(
                $"Erro crítico no domínio da aplicação:\n\n{exception.Message}\n\n" +
                "A aplicação será encerrada.",
                "Erro Crítico",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private static void LogException(Exception exception, string errorType = "ERROR")
    {
        try
        {
            var logsPath = Path.Combine(AppContext.BaseDirectory, "logs");
            Directory.CreateDirectory(logsPath);
            
            var logFile = Path.Combine(logsPath, "app.log");
            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {errorType}:\n" +
                          $"Mensagem: {exception.Message}\n" +
                          $"Tipo: {exception.GetType().FullName}\n" +
                          $"StackTrace:\n{exception.StackTrace}\n";
            
            if (exception.InnerException != null)
            {
                logEntry += $"\nInnerException: {exception.InnerException.Message}\n" +
                           $"InnerType: {exception.InnerException.GetType().FullName}\n" +
                           $"InnerStackTrace:\n{exception.InnerException.StackTrace}\n";
            }
            
            logEntry += "\n" + new string('=', 80) + "\n\n";
            
            File.AppendAllText(logFile, logEntry);
        }
        catch
        {
            // Se não conseguir escrever no log, ignore
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}