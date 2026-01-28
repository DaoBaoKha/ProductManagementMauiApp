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

    public ObservableCollection<BannerDto> Banners { get; } = new();

    public MainPageViewModel(INavigationService navigationService)
    {
        Title = "Main Page";
        _navigationService = navigationService;

        LoadBanners();
    }

    [RelayCommand]
    async Task GoToUserManagePage()
    {
        await _navigationService.NavigateToAsync<UserManagePageViewModel>();
    }

    [RelayCommand]
    async Task GoToProductManagePage()
    {
        await _navigationService.NavigateToAsync<ProductManagePageViewModel>();
    }

    private void LoadBanners()
    {
        Banners.Clear();

        Banners.Add(new BannerDto
        {
            Title = "Welcome Bao Kha",
            ImageUrl = "dotnet_bot.png",
            Message = "Welcome"
        });

        Banners.Add(new BannerDto
        {
            Title = "New Feature",
            ImageUrl = "dotnet_bot.png",
            Message = "Feature"
        });

        Banners.Add(new BannerDto
        {
            Title = "Upcoming Event",
            ImageUrl = "dotnet_bot.png",
            Message = "Event"
        });

        Banners.Add(new BannerDto
        {
            Title = "About Us",
            ImageUrl = "dotnet_bot.png",
            Message = "Information"
        });
    }
}
