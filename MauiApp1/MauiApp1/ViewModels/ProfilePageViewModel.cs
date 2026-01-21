using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.AppLogic.DTOs;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        ProfileDto selectedProfile;

        public ObservableCollection<ProfileDto> Profiles { get; } = new();

        public ProfilePageViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            Title = "Profile";
            _dialogService = dialogService;
            _navigationService = navigationService;

            Task.Run(async () => await LoadProfile());
        }


        async Task LoadProfile()
        {
            IsRefreshing = true;
            await Task.Delay(2000); // Simulate a data load

            Profiles.Clear();

            SelectedProfile = new ProfileDto
            {
                Id = 1,
                Fullname = "Dao Bao Kha",
                Email = "daobaokha2509@gmail.com",
                JobTitle = "Backend Developer",
                PhoneNumber = "+84 123 456 789",
                Address = "123 Main St, City, Country",
                AvatarUrl = "https://static.vecteezy.com/system/resources/thumbnails/011/675/374/small_2x/man-avatar-image-for-profile-png.png",
                DateOfBirth = new DateOnly(2004, 9, 25),
            };

            isRefreshing = false;
        }

        [RelayCommand]
        async Task ViewImage()
        {
            if (SelectedProfile != null && !string.IsNullOrEmpty(SelectedProfile.AvatarUrl))
            {
                var parameters = new Dictionary<string, object>
                {
                    { "ImageUrl", SelectedProfile.AvatarUrl }
                };

                await _navigationService.NavigateToAsync<ProfileImagePageViewModel>(parameters);
            }
        }

    }
}
