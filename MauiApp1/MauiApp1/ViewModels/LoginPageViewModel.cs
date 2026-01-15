using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;

namespace MauiApp1.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public LoginPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            Title = "Login Page";
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task Login()
        {
            if(username == "admin" && password == "12345")
            {
                await _dialogService.ShowAlertAsync("Login", "Login successful!");
                // Clear navigation stack to prevent going back to login page
                await _navigationService.NavigateToAsyncAndClearStack<MainPageViewModel>();
            }
            else
            {
                await _dialogService.ShowAlertAsync("Login", "Invalid username or password.");
            }
        }
    }
}
