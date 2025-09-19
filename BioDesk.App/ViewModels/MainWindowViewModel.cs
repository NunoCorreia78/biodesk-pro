using CommunityToolkit.Mvvm.ComponentModel;
using BioDesk.App.Services;
using BioDesk.App.ViewModels.Base;

namespace BioDesk.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public INavigationService NavigationService { get; }

    public MainWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}