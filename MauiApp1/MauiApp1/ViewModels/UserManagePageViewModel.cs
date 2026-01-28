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
        
        // Store all users for search filtering
        private List<UserDto> AllUsers { get; set; } = new();

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

            AllUsers.Clear();
            Users.Clear();

            var user1 = new UserDto
            {
                Id = "1",
                Username = "daobaokha",
                Email = "daobaokha@gmail.com",
                FullName = "Dao Bao Kha",
                DateOfBirth = new DateTime(2004, 09, 25),
                AvatarUrl = "https://example.com/avatar1.png"
            };

            var user2 = new UserDto
            {
                Id = "2",
                Username = "baokha",
                Email = "baokha@gmail.com",
                FullName = "Bao Kha",
                DateOfBirth = new DateTime(2004, 09, 24),
                AvatarUrl = "https://example.com/avatar1.png"
            };

            AllUsers.Add(user1);
            AllUsers.Add(user2);
            
            Users.Add(user1);
            Users.Add(user2);

            IsRefreshing = false;
        }

        [RelayCommand]
        void SearchUser(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Show all users if search is empty
                Users.Clear();
                foreach (var user in AllUsers)
                {
                    Users.Add(user);
                }
            }
            else
            {
                // Filter users by name, email, or username
                var filteredUsers = AllUsers.Where(u =>
                    u.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    u.Username.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                Users.Clear();
                foreach (var user in filteredUsers)
                {
                    Users.Add(user);
                }
            }
        }

        [RelayCommand]
        async Task DeleteUser(UserDto user)
        {
            if (user == null)
                return;

            AllUsers.Remove(user);
            Users.Remove(user);
            
            await Application.Current.MainPage.DisplayAlert("Success", $"User {user.FullName} deleted!", "OK");
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
