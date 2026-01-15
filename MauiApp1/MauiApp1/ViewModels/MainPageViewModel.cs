using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.AppLogic.DTOs;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    bool isRefreshing;

    public ObservableCollection<UserDto> Users { get; } = new();

    public MainPageViewModel(INavigationService navigationService)
    {
        Title = "Main Page";
        _navigationService = navigationService;
    }

    [RelayCommand]
    async Task GoToUserManagePage()
    {
        await _navigationService.NavigateToAsync<UserManagePageViewModel>();
    }
}
