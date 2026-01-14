using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;

namespace MauiApp1.ViewModels;

public partial class DemoPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public DemoPageViewModel(INavigationService navigationService)
    {
        Title = "Demo Page";
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task GoToMainPage()
    {
        // Navigate back to MainPage
        await _navigationService.GoBackAsync();
    }
}
