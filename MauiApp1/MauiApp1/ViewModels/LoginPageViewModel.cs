using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;

namespace MauiApp1.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private LoginPage? _loginPage;

        public LoginPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            Title = "Login Page";
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        // Method to set page reference (called from LoginPage constructor)
        public void SetPage(LoginPage page)
        {
            _loginPage = page;
        }

        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [RelayCommand]
        async Task Login()
        {
            if(username == "admin" && password == "12345")
            {
                // Pass login=true parameter to trigger success banner on MainPage
                var parameters = new Dictionary<string, object>
                {
                    { "toast_message", "Login Successfully!" }
                };
                
                await _navigationService.NavigateToAsyncAndClearStack<MainPageViewModel>(parameters);
            }
            else
            {
                await _dialogService.ShowAlertAsync("Login", "Invalid username or password.");
            }
        }
    }
}
