using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.AppLogic.DTOs;
using MauiApp1.AppLogic.Enums;
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

        [ObservableProperty]
        string searchText;
        
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

        // Auto-search when SearchText changes
        partial void OnSearchTextChanged(string value)
        {
            SearchUser(value);
        }

        [RelayCommand]
        async Task LoadUser()
        {
            IsRefreshing = true;
            await Task.Delay(2000);

            var newUsers = new List<UserDto>
            {
                new UserDto
                {
                    Id = "1",
                    Username = "daobaokha",
                    Email = "daobaokha@gmail.com",
                    FullName = "Dao Bao Kha",
                    DateOfBirth = new DateTime(2004, 09, 25),
                    Gender = Gender.Male,
                    AvatarUrl = "https://img.freepik.com/premium-vector/character-avatar-isolated_729149-194801.jpg"
                },
                new UserDto
                {
                    Id = "2",
                    Username = "user2",
                    Email = "user2@gmail.com",
                    FullName = "Nguyen Van A",
                    DateOfBirth = new DateTime(2015, 01, 01),
                    Gender = Gender.Male,
                    AvatarUrl = "https://img.freepik.com/premium-vector/character-avatar-isolated_729149-194802.jpg"
                },
                new UserDto
                {
                     Id = "3",
                     Username = "baokha",
                     Email = "baokha@gmail.com",
                     FullName = "Bao Kha",

                     DateOfBirth = new DateTime(2004, 09, 24),
                     Gender = Gender.Female,
                     AvatarUrl = "https://img.freepik.com/premium-vector/character-avatar-isolated_729149-194802.jpg"
                },
                new UserDto
                {
                     Id = "4",
                     Username = "baokha",
                     Email = "baokha@gmail.com",
                     FullName = "Bao Kha",
                     DateOfBirth = new DateTime(2005, 09, 24),
                     Gender = Gender.Other,
                     AvatarUrl = "https://img.freepik.com/premium-vector/character-avatar-isolated_729149-194802.jpg"
                },
                new UserDto
                {
                     Id = "5",
                     Username = "baokha",
                     Email = "baokha@gmail.com",
                     FullName = "Bao Kha",
                     DateOfBirth = new DateTime(1994, 09, 24),
                     Gender = Gender.PreferNotToSay,
                     AvatarUrl = "https://img.freepik.com/premium-vector/character-avatar-isolated_729149-194802.jpg"
                },
            };

            // return to main thread to update UI
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AllUsers = new List<UserDto>(newUsers);

                Users.Clear();
                foreach (var u in newUsers)
                {
                    Users.Add(u);
                }

                IsRefreshing = false;
            });
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

        // Edit Mode Logic
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEditing))]
        bool isEditing;

        public bool IsNotEditing => !IsEditing;

        [RelayCommand]
        void EnableEditMode()
        {
            if (!IsEditing) IsEditing = true;
        }

        [RelayCommand]
        async Task SaveUser()
        {
            // Simulate Save
            IsEditing = false;
            // Call API to update SelectedUser
            await Application.Current.MainPage.DisplayAlert("Success", "User Updated", "OK");
        }

        [RelayCommand]
        void CancelEdit()
        {
            IsEditing = false;
            // Revert logic would go here
        }
    }
}
