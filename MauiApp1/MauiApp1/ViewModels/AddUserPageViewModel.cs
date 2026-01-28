using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiApp1.Messages;
using MauiApp1.Services;
using System.Text.RegularExpressions;

namespace MauiApp1.ViewModels
{
    public partial class AddUserPageViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        // Email regex pattern
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public AddUserPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            Title = "Add User Page";
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNameValid), nameof(IsFormValid))]
        private string name = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEmailValid), nameof(IsFormValid))]
        private string email = string.Empty;

        /// <summary>
        /// Name is valid if not empty and contains no numbers
        /// </summary>
        public bool IsNameValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return false;
                return !Name.Any(char.IsDigit);
            }
        }

        /// <summary>
        /// Email is valid if not empty and matches email format
        /// </summary>
        public bool IsEmailValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Email))
                    return false;
                return EmailRegex.IsMatch(Email);
            }
        }

        /// <summary>
        /// Form is valid when both Name and Email are valid
        /// </summary>
        public bool IsFormValid => IsNameValid && IsEmailValid;

        [RelayCommand]
        async Task SaveUser()
        {
            WeakReferenceMessenger.Default.Send(new ShowToastMessage($"User '{Name}' added successfully!"));

            // Clear form after save
            Name = string.Empty;
            Email = string.Empty;
        }
    }
}
