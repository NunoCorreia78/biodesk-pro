using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BioDesk.App.Services;
using BioDesk.App.ViewModels.Base;
using System;
using System.Windows.Threading;

namespace BioDesk.App.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly DispatcherTimer _timeTimer;

    [ObservableProperty]
    private DateTime currentTime = DateTime.Now;

    public HomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        // Timer para atualizar a hora
        _timeTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timeTimer.Tick += (s, e) => CurrentTime = DateTime.Now;
        _timeTimer.Start();
    }

    [RelayCommand]
    private void OpenPacientes()
    {
        _navigationService.GoTo("Pacientes");
    }

    [RelayCommand]
    private void OpenIrisAnonima()
    {
        _navigationService.GoTo("IrisAnonima");
    }
}