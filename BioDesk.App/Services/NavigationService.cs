using System.ComponentModel;

namespace BioDesk.App.Services;

public interface INavigationService
{
    event PropertyChangedEventHandler? PropertyChanged;
    string CurrentView { get; }
    void GoTo(string viewName);
}

public class NavigationService : INavigationService, INotifyPropertyChanged
{
    private string _currentView = "Home";

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

    public void GoTo(string viewName)
    {
        if (IsValidView(viewName))
        {
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

    private static bool IsValidView(string viewName)
    {
        return viewName is "Home" or "Pacientes" or "IrisAnonima";
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}