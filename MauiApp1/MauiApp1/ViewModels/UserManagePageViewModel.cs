using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.AppLogic.DTOs;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public partial class UserManagePageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool isPopupOpen;

        [ObservableProperty]
        UserDto selectedUser;

        public ObservableCollection<UserDto> Users { get; } = new();

        public UserManagePageViewModel(INavigationService navigationService)
        {
            Title = "User Manage Page";
            _navigationService = navigationService;

            // Load users when page initializes
            Task.Run(async () => await LoadUser());
        }

        [RelayCommand]
        async Task LoadUser()
        {
            IsRefreshing = true;
            await Task.Delay(2000); // Simulate a data load

            Users.Clear();

            Users.Add(new UserDto
            {
                Id = "1",
                Username = "daobaokha",
                Email = "daobaokha@gmail.com",
                FullName = "Dao Bao Kha",
                DateOfBirth = new DateTime(2004, 09, 25),
                AvatarUrl = "https://example.com/avatar1.png"
            });

            Users.Add(new UserDto
            {
                Id = "2",
                Username = "baokha",
                Email = "baokha@gmail.com",
                FullName = "Bao Kha",
                DateOfBirth = new DateTime(2004, 09, 24),
                AvatarUrl = "https://example.com/avatar1.png"
            });

            IsRefreshing = false;
        }

        [RelayCommand]
        async Task GoToAddUserPage()
        {
            await _navigationService.NavigateToAsync<AddUserPageViewModel>();
        }

        [RelayCommand]
        void ShowUserDetails(UserDto user)
        {
            if(user == null)
                return;

            SelectedUser = user;
            IsPopupOpen = true;
        }

        [RelayCommand]
        void ClosePopup()
        {
            IsPopupOpen = false;
            SelectedUser = null;
        }
    }
}
