using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;

namespace MauiApp1.ViewModels;

public partial class DashboardBarViewModel : BaseViewModel
{
    private readonly IDialogService _dialogService;
    private readonly INavigationService _navigationService;

    public DashboardBarViewModel(IDialogService dialogService, INavigationService navigationService)
    {
        Title = "Dashboard";
        _dialogService = dialogService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task Home()
    {
        await _dialogService.ShowAlertAsync("Dashboard", "Home clicked");
    }

    [RelayCommand]
    private async Task Profile()
    {
        await _navigationService.NavigateToAsync<DemoPageViewModel>();
        await _dialogService.ShowAlertAsync("Dashboard", "Profile clicked");
    }

    [RelayCommand]
    private async Task Settings()
    {
        await _dialogService.ShowAlertAsync("Dashboard", "Settings clicked");
    }

    [RelayCommand]
    private async Task Logout()
    {
        var result = await _dialogService.ShowConfirmAsync(
            "Logout", 
            "Are you sure you want to logout?");
        
        if (result)
        {
            await _navigationService.NavigateToAsyncAndClearStack<LoginPageViewModel>();
            await _dialogService.ShowAlertAsync("Logout", "Logged out successfully");
        }
    }
}
