using System.ComponentModel;

namespace BioDesk.App.Services;

public interface INavigationService
{
    event PropertyChangedEventHandler? PropertyChanged;
    string CurrentView { get; }
    object? NavigationParameter { get; }
    void GoTo(string viewName, object? parameter = null);
}

public class NavigationService : INavigationService, INotifyPropertyChanged
{
    private string _currentView = "Home";
    private object? _navigationParameter;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string CurrentView
    {
        get => _currentView;
        private set
        {
            if (_currentView != value)
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
    }

    public object? NavigationParameter
    {
        get => _navigationParameter;
        private set
        {
            if (_navigationParameter != value)
            {
                _navigationParameter = value;
                OnPropertyChanged(nameof(NavigationParameter));
            }
        }
    }

    public void GoTo(string viewName, object? parameter = null)
    {
        if (IsValidView(viewName))
        {
            NavigationParameter = parameter;
            CurrentView = viewName;
        }
    }

    public void NavigateToHome()
    {
        GoTo("Home");
    }

    public void NavigateToPacientes()
    {
        GoTo("Pacientes");
    }

    public void NavigateToIrisAnonima()
    {
        GoTo("IrisAnonima");
    }

    public void NavigateToFichaPaciente(int pacienteId = 0)
    {
        System.Diagnostics.Debug.WriteLine($"*** NavigateToFichaPaciente chamado com ID: {pacienteId} ***");
        GoTo("FichaPaciente", pacienteId);
        System.Diagnostics.Debug.WriteLine($"GoTo executado. CurrentView: {CurrentView}, Parameter: {NavigationParameter}");
    }

    public void NavigateToQuestionarioCompleto()
    {
        GoTo("QuestionarioCompleto");
    }

    private static bool IsValidView(string viewName)
    {
        return viewName is "Home" or "Pacientes" or "IrisAnonima" or "FichaPaciente" or "QuestionarioCompleto";
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}