using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;

namespace MauiApp1.ViewModels;

public partial class DashboardBarViewModel : BaseViewModel
{
    private readonly IDialogService _dialogService;

    public DashboardBarViewModel(IDialogService dialogService)
    {
        Title = "Dashboard";
        _dialogService = dialogService;
    }

    [RelayCommand]
    private async Task Home()
    {
        await _dialogService.ShowAlertAsync("Dashboard", "Home clicked");
    }

    [RelayCommand]
    private async Task Profile()
    {
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
            await _dialogService.ShowAlertAsync("Logout", "Logged out successfully");
        }
    }
}
