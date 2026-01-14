using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;

namespace MauiApp1.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private int counter;

    [ObservableProperty]
    private string counterText = "Click me";

    public MainPageViewModel(INavigationService navigationService)
    {
        Title = "Main Page";
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task GoToDemoPage()
    {
        // Navigate to DemoPage using NavigationService
        await _navigationService.NavigateToAsync<DemoPageViewModel>();
    }

    [RelayCommand]
    private async Task CounterClick()
    {
        Counter++;

        CounterText = $"You clicked {Counter} time{(Counter == 1 ? "" : "s")}";

        await Task.CompletedTask;
    }
}
