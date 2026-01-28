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

        // Edit mode state
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEditing))]
        [NotifyPropertyChangedFor(nameof(ButtonText))]
        private bool isEditing = false;

        // Read-only mode (inverse of IsEditing)
        public bool IsNotEditing => !IsEditing;

        // Button text based on edit state
        public string ButtonText => IsEditing ? "Save Changes" : "Edit Profile";

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

        [RelayCommand]
        async Task ChangeAvatar()
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                    if (photo != null)
                    {
                        // Save the file to a local path or simply use the FullPath
                        // For a real app, you might want to copy it to AppDataDirectory
                        var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                        using var stream = await photo.OpenReadAsync();
                        using var newStream = File.OpenWrite(newFile);
                        await stream.CopyToAsync(newStream);

                        SelectedProfile.AvatarUrl = newFile;
                        // Force property change notification if needed, usually ObservableProperty handles it 
                        // but since it's a property of a property, we might need to refresh or ensure ProfileDto implements INotifyPropertyChanged
                        // For now assuming simple binding update works or we might need to re-assign SelectedProfile mechanism.
                        // Actually SelectedProfile is ObservableProperty, but its fields might not raise notification. 
                        // To be safe, let's trigger a refresh or re-assign.
                        OnPropertyChanged(nameof(SelectedProfile));
                    }
                }
                else
                {
                    await _dialogService.ShowAlertAsync("Error", "Picking photos is not supported on this device.", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync("Error", $"Unable to pick photo: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        async Task ToggleEdit()
        {
            if (IsEditing)
            {
                await _dialogService.ShowAlertAsync("Success", "Profile updated!", "OK");

                // Exit edit mode
                IsEditing = false;
            }
            else
            {
                // Enter edit mode
                IsEditing = true;
            }
        }

        // double click to enable edit mode
        [RelayCommand]
        void EnableEditMode()
        {
            if (!IsEditing)
            {
                IsEditing = true;
            }
        }

    }
}
